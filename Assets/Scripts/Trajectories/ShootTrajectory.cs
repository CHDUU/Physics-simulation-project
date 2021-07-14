using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RegisteredBall
{
    public Ball real;
    public Ball hidden;
}

public class ShootTrajectory : MonoBehaviour
{
    public GameObject ball;
    public GameObject marker;
    public Transform referenceBall;

    private Scene mainScene;
    private Scene physicsScene;

    private List<GameObject> markers = new List<GameObject>();
    private Dictionary<string, RegisteredBall> allBalls = new Dictionary<string, RegisteredBall>();

    public GameObject objectsToSpawn;

    void Start()
    {
        Physics.autoSimulation = false;
        mainScene = SceneManager.GetActiveScene();
        physicsScene = SceneManager.CreateScene("physics-scene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

        PreparePhysicsScene();
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            ShowTrajectory();
        }

        mainScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
        
    }

    public void RegisterBall(Ball ball)
    {
        if (!allBalls.ContainsKey(ball.gameObject.name))
        {
            allBalls[ball.gameObject.name] = new RegisteredBall();
        }

        var balls = allBalls[ball.gameObject.name];
        if (string.Compare(ball.gameObject.scene.name, physicsScene.name) == 0)
        {
            balls.hidden = ball;
        }
        else
        {
            balls.real = ball;
        }

        allBalls[ball.gameObject.name] = balls;

    }

    public void PreparePhysicsScene()
    {
        SceneManager.SetActiveScene(physicsScene);

        GameObject g = GameObject.Instantiate(objectsToSpawn);
        g.transform.name = "ReferenceBall";
        g.GetComponent<Ball>().isReference = true;

        Destroy(g.GetComponent<MeshRenderer>());

        SceneManager.SetActiveScene(mainScene);
    }

    public void CreateMovementMarkers()
    {
        foreach (var ballType in allBalls)
        {
            var balls = ballType.Value;
            Ball hidden = balls.hidden;

            GameObject g = GameObject.Instantiate(marker, hidden.transform.position, Quaternion.identity);
            g.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            markers.Add(g);
        }
    }

    public void ShowTrajectory()
    {
        SyncBalls();

        allBalls["ReferenceBall"].hidden.transform.rotation = referenceBall.transform.rotation;
        allBalls["ReferenceBall"].hidden.GetComponent<Rigidbody>().velocity = referenceBall.transform.TransformDirection(Vector3.up * 15f);
        allBalls["ReferenceBall"].hidden.GetComponent<Rigidbody>().useGravity = true;

        int steps = (int)(2f / Time.fixedDeltaTime);

        for(int i = 0; i < steps; i++)
        {
            physicsScene.GetPhysicsScene().Simulate(Time.fixedDeltaTime);
            CreateMovementMarkers();
        }
    }

    public void SyncBalls()
    {
        foreach (var ballType in allBalls)
        {
            var balls = ballType.Value;
            Ball visual = balls.real;
            Ball hidden = balls.hidden;
            var rb = hidden.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            hidden.transform.position = visual.transform.position;
            hidden.transform.rotation = visual.transform.rotation;
        }
    }

}
