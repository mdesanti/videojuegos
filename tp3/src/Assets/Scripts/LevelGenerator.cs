using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public GameObject wall;
	public GameObject floor;
	public GameObject door;
	public GameObject torch;
	private int width = 400;
	private int height = 400;
	private int start_x = 5;
	private int start_y = 5;
	private int x = 0;
	private int z = 0;
	private int seed = 1234;
	private enum Directions {LEFT, RIGHT, TOP, DOWN};
	private Directions actual;
	private Directions previous;
	private int step = 10;
	private bool create = true;
	private double elapsedTime = 0;
	private bool room_created = false;
	private bool direction_changed = false;
	private int default_size = 5;
	private bool torch_created = true;


    void Start() {
        CreateLevel();
    } 

	void CreateLevel() {
		Random.seed = seed;
		actual = previous = Directions.RIGHT;
        while(create && Mathf.Abs(x) < width && Mathf.Abs(z) < height && elapsedTime < 5) {
        	elapsedTime += Time.deltaTime;
        	if(!checkSpace(actual, default_size)) {
        		if(!changeDirection()) {
        			create = false;
                    break;
                }
        		else 
        			direction_changed = true;
        	}

    		if(Random.value > 0.8f) {
    			createRoom();
    			room_created = true;
    		} else {
    			if(Random.value > 0.6f) {
    				if(changeDirection())
    					direction_changed = true;
    			}
    			putCorridor(x, z);
    		}
    		if(!room_created) {
	    		if(actual == Directions.RIGHT || actual == Directions.LEFT) {
	    			x += step;
	    		} else {
	        		z += step;
	    		}
    		}
    		room_created = direction_changed = false;
        }
    }

    private bool changeDirection() {
    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
            if(checkSpace(actual, default_size)) {
                putCorridor(x, z);
                x += step;
            }
    		if(Random.value > 0.5f) {
    			if(checkSpace(Directions.DOWN, default_size)) {
    				previous = actual;
	   				actual = Directions.DOWN;
	   				step = -10;
	   			} else
	   		          return false;
   			} else {
   				if(checkSpace(Directions.TOP, default_size)) {
   					previous = actual;
	   				actual = Directions.TOP;
	   				step = 10;
   				} else
                    return false;
            }
            return true;
        } else  {
            if(checkSpace(actual, default_size)) {
                putCorridor(x, z);
                z += step;
            }
    		if( Random.value > 0.5f) {
    			if(checkSpace(Directions.LEFT, default_size)) {
    				previous = actual;
	    			actual = Directions.LEFT;
	    			step = -10;
	    		} else
	    		     return false;
    		}
    		else {
    			if(checkSpace(Directions.RIGHT, default_size)) {
    				previous = actual;
	    			actual = Directions.RIGHT;
	    			step = 10;
    			} else
    			     return false;
    		}
            return true;
        }
    }

    private bool checkSpace(Directions direction, int size) {
    	Vector3 d = new Vector3(0, 0, 0);
    	UnityEngine.RaycastHit hitInfo = new RaycastHit();
    	int sign = 1;
    	if(direction == Directions.LEFT) {
    		d = new Vector3(-1, 0, 0);
    		sign = -1;
    	}
    	if(direction == Directions.RIGHT) {
    		d = new Vector3(1, 0, 0);
    	}
    	if(direction == Directions.TOP) {
    		d = new Vector3(0, 0, 1);
    	}
    	if(direction == Directions.DOWN) {
    		d = new Vector3(0, 0, -1);
    		sign = -1;
    	} 	
        if(Physics.Raycast(new Vector3(x, 5, z), d, out hitInfo, 10f * size)) {
    			//Debug.Log("collision:" + hitInfo.collider.tag + "x:" + x + " z:" + z);
    			return false;
        }
    	//Debug.Log("not collision");
    	return true;
    }


    private void putCorridor(int x, int z) {
    	putFloor(x, z);
    	GameObject w1 = (GameObject)GameObject.Instantiate(wall);
    	GameObject w2 = (GameObject)GameObject.Instantiate(wall);
    	if(actual == Directions.LEFT || actual == Directions.RIGHT) {
	    	if(direction_changed) {
	    		if(previous == Directions.DOWN) {
    					w1.transform.position = new Vector3(x, 6, z - 5);
    			} else if(previous == Directions.TOP) {
						w1.transform.position = new Vector3(x, 6, z + 5);
    			}
    			if(actual == Directions.RIGHT) {
					w2.transform.position = new Vector3(x - 5, 6, z);
                    w2.transform.eulerAngles = new Vector3(0, 90, 0);
                    if(torch_created) {
                        putTorch(x - 5, z, new Vector3(0, 0, -30));
                    }
    			} else { 
    				w2.transform.position = new Vector3(x + 5, 6, z);
                    w2.transform.eulerAngles = new Vector3(0, 90, 0);
                    if(torch_created) {
                        putTorch(x + 5, z, new Vector3(0, 0, 30));
                    }
                }
    		} else {
    			if(torch_created) {
    				putTorch(x, z + 5, new Vector3(-30, 0, 0));
    				putTorch(x, z - 5, new Vector3(30, 0, 0));
    			}
		    	w1.transform.position = new Vector3(x, 6, z + 5);
		    	w2.transform.position = new Vector3(x, 6, z - 5);
    		}
    	} else {
    		if(direction_changed) {
				if(previous == Directions.RIGHT) {
					w1.transform.position = new Vector3(x + 5, 6, z);
                    w1.transform.eulerAngles = new Vector3(0, 90, 0);
				} else if(previous == Directions.LEFT) {
					w1.transform.position = new Vector3(x - 5, 6, z);
                    w1.transform.eulerAngles = new Vector3(0, 90, 0);
	    		}
	    		if(actual == Directions.DOWN) {
	    				w2.transform.position = new Vector3(x, 6, z + 5);
                        if(torch_created) {
                            putTorch(x, z + 5, new Vector3(-30, 0, 0));
                        }
	    		} else {
	    				w2.transform.position = new Vector3(x, 6, z - 5);
                        if(torch_created) {
                            putTorch(x, z - 5, new Vector3(30, 0, 0));
                        }
	    		}
    		} else {
    			if(torch_created) {
    				putTorch(x + 5, z, new Vector3(0, 0, 30));
    				putTorch(x - 5, z, new Vector3(0, 0, -30));
    			}
		    	w1.transform.position = new Vector3(x + 5, 6, z);
                w2.transform.position = new Vector3(x - 5, 6, z);
                w1.transform.eulerAngles = new Vector3(0, 90, 0);
                w2.transform.eulerAngles = new Vector3(0, 90, 0);
	    	}
    	}
    	torch_created = !torch_created;
    }

    private bool createRoom() {
    	int width = ((int) (Random.value * 5) + 5) * 10;
    	int height = ((int) (Random.value * 5) + 5) * 10;

    	if(!checkSpace(actual, Mathf.Max(width, height) + 10))
    		return false;
    	if(actual == Directions.RIGHT){
    		if(!checkSpace(Directions.TOP, Mathf.Max(width, height) + 10))
    			return false;
    	}
    	if(actual == Directions.LEFT){
    		if(!checkSpace(Directions.DOWN, Mathf.Max(width, height) + 10))
    			return false;
    	}
    	if(actual == Directions.DOWN){
    		if(!checkSpace(Directions.LEFT, Mathf.Max(width, height) + 10))
    			return false;
    	}
    	if(actual == Directions.TOP){
    		if(!checkSpace(Directions.RIGHT, Mathf.Max(width, height) + 10))
    			return false;
    	}

    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
    		putDoor(x - step / 2, z);
    	} else {
    		putDoor(x, z - step / 2);
    	}
    	int sign = step / Mathf.Abs(step);
        bool other_torch = true;
    	for(int i = 0; Mathf.Abs(i) < height; i+= step) {
        	for(int j = 0; Mathf.Abs(j) < width; j+= step) {
        		putFloor(x + j, z + i);
        		if(j == 0) {
        			GameObject w = (GameObject)GameObject.Instantiate(wall);
	    			w.transform.position = new Vector3(x + j - 5 * sign, 6, z + i);
	    			w.transform.eulerAngles = new Vector3(0, 90, 0);
                    if(other_torch) {
                        putTorch(x + j - 5, z + i, new Vector3(0, 0, -30));
                    }
                        //other_torch = !other_torch;
                }
                if (Mathf.Abs(j) == width - 10) {
                    GameObject w = (GameObject)GameObject.Instantiate(wall);
                    w.transform.position = new Vector3(x + j + 5 * sign, 6, z + i);
	    			w.transform.eulerAngles = new Vector3(0, 90, 0);
                    if(!other_torch) {
                        putTorch(x + j + 5, z + i, new Vector3(0, 0, 30));
                    }
                        other_torch = !other_torch;
                } 
                if (i == 0) {
                    GameObject w = (GameObject)GameObject.Instantiate(wall);
                    w.transform.position = new Vector3(x + j, 6, z + i - 5 * sign);
                    if(torch_created) {
                        putTorch(x + j, z + i - 5, new Vector3(30, 0, 0));
                    }
                        torch_created = !torch_created;
                } 
                if (Mathf.Abs(i) == height - 10) {
                    GameObject w = (GameObject)GameObject.Instantiate(wall);
                    w.transform.position = new Vector3(x + j, 6, z + i + 5 * sign);
                    if(torch_created) {
                        putTorch(x + j, z + i + 5, new Vector3(-30, 0, 0));
                    }
                        torch_created = !torch_created;
        		}
        	}
        }


    	x += width * (step / Mathf.Abs(step));
    	z += height * (step / Mathf.Abs(step));
  
    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
    		z -= step;
            //int sign = (step / Mathf.Abs(step));
    		putDoor(x - step / 2, z); //salida
            /*if(Random.value > 0.5) {
                //extra door
                if(Random.value > 0.5) {
                    int rand = 1 +  ((int) Random.value * (width - 1));
                    GameObject d = putDoor(x - rand * 10 * sign, z - (step / 2)); //arriba
                    d.transform.eulerAngles = new Vector3(90, 0, 0);
                    Debug.Log("arriba");
                }
                else {
                    int rand = 1 +  ((int) Random.value * (width - 1));
                    GameObject d = putDoor(x - rand * 10 * sign, z + step / 2); //abajo
                    d.transform.eulerAngles = new Vector3(90, 0, 0);
                    Debug.Log("abajo");
                }
            }*/
    	} else {
    		x -= step;
    		putDoor(x, z - step / 2); //salida
            /*if(Random.value > 0.5) {
                if(Random.value > 0.5) {
                    int rand = 1 +  ((int) Random.value * (height - 1));
                    putDoor(x - step / 2, z - rand); //derecha
                    Debug.Log("derecha");
                }
                else {
                    int rand = 1 +  ((int) Random.value * (height - 1));
                    putDoor(x - width * (step / Mathf.Abs(step)) + step / 2, z - rand * 10); //abajo
                    Debug.Log("izquierda");
                }
            }*/
    	}
    	return true;
    }

    private GameObject putDoor(int x, int z) {
		Vector3 rotation;
    	if(actual == Directions.RIGHT || actual == Directions.LEFT)
    		rotation = new Vector3(90, 90, 0);
    	else
    		rotation = new Vector3(90, 0, 0);
    	GameObject d = (GameObject)GameObject.Instantiate(door);
    	d.transform.position = new Vector3(x, 10, z);
    	d.transform.eulerAngles = rotation;
        return d;
    }

    private void putFloor(int x, int z) {
    	GameObject f = (GameObject)GameObject.Instantiate(floor);
    	f.transform.position = new Vector3(x, 0, z);
    }

    private void putTorch(int x, int z, Vector3 rotation) {
    	GameObject t = (GameObject)GameObject.Instantiate(torch);
    	t.transform.position = new Vector3(x, 5, z);
    	t.transform.eulerAngles = rotation;
    }

}