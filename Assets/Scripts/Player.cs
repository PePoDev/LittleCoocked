using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private GameInput gameInput;
	
	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float rotateSpeed = 10f;
	
	private bool isWalking; 
	
	private void Update() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
	    
		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
	    
		isWalking = moveDir != Vector3.zero;
		
		if (isWalking == false) {
			return;
		}
	    
		transform.position += moveDir * moveSpeed * Time.deltaTime;
	    
	    transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
	}
    
	public bool IsWalking() {
		return isWalking;
	}
}
