using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//responsible for the camera position which is implies the angle the user will see the game at
public class CameraController : MonoBehaviour
{


    
	Transform _player;
	public Vector3 initialPos;
    public Vector3 startPosition;
    public float deltaY;

	void Start() {
		_player = GameObject.FindGameObjectWithTag("Player").transform;

		initialPos = this.transform.position;
		deltaY = _player.position.y - initialPos.y;
	}

	void LateUpdate() {
		float playerY = _player.position.y;

		if (transform.position.y + deltaY > playerY) {
			initialPos.y = playerY - deltaY;
			transform.position = initialPos;
		}
	}

   public void resetCamera()
    {
        this.transform.position =new Vector3(-0.14f, 27.28f, -8.75f);

    }
}
