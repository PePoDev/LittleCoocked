using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField] private GameInput gameInput;
	
	[SerializeField] private float moveSpeed = 7f;
	[SerializeField] private float rotateSpeed = 10f;
	
	private bool isWalking; 
	
	private void Update() {
		HandleMovement();
		//HandleInteraction();
	}
    
	public bool IsWalking() {
		return isWalking;
	}
	
	//private void HandleInteraction() {
	//	Vector2 inputVector = gameInput.GetMovementVectorNormalized();
		
	//	Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
		
	//	float interactDistance = 2f;
		
	//	if (Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance)){
	//		Debug.Log(raycastHit.transform);
	//	}
	//}
	
	private void HandleMovement() {
		Vector2 inputVector = gameInput.GetMovementVectorNormalized();
	    
		Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
		isWalking = moveDir != Vector3.zero;
		
		if (isWalking == false) {
			return;
		}
		
		float moveDistance = moveSpeed * Time.deltaTime;
		float playerRadius = 0.7f;
		float playerHeight = 2f;
		
		//bool canMove = !Physics.Raycast(transform.position, moveDir, moveDistance);
		bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
	    
		if (!canMove) {
			Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
			canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
			
			if (canMove) {
				moveDir = moveDirX;
			} else {
				Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
				
				if (canMove) {
					moveDir = moveDirZ;
				}
			}
		}
	    
		if (canMove) {
			transform.position += moveDir * moveDistance;
		}
	    
		transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
	}
}
