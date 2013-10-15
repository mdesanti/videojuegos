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
	private int y = 0;
	private int x = 0;
	private int seed = 123;
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
		Random.seed = seed;
		actual = previous = Directions.RIGHT;
        while(create && Mathf.Abs(x) < width && Mathf.Abs(y) < height && elapsedTime < 5) {
        	elapsedTime += Time.deltaTime;
        	if(!checkSpace(actual, default_size)) {
        		if(!changeDirection())
        			create = false;
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
    			putCorridor(x, y);
    		}
    		if(!room_created) {
	    		if(actual == Directions.RIGHT || actual == Directions.LEFT) {
	    			x += step;
	    		} else {
	        		y += step;
	    		}
    		}
    		room_created = direction_changed = false;
        }
    }

    private bool changeDirection() {
    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
            if(checkSpace(actual, default_size)) {
    	  		putCorridor(x, y);
        		x += step;
            }
    		if(Random.value > 0.5f) {
    			if(checkSpace(Directions.DOWN, default_size)) {
    				previous = actual;
	   				actual = Directions.DOWN;
	   				step = -10;
	   				return true;
	   			}
	   			return false;
   			} else {
   				if(checkSpace(Directions.TOP, default_size)) {
   					previous = actual;
	   				actual = Directions.TOP;
	   				step = 10;
	   				return true;
   				}
   			}
                return false;
        } else  {
            if(checkSpace(actual, default_size)) {
            	putCorridor(x, y);
            	y += step;
            }
    		if( Random.value > 0.5f) {
    			if(checkSpace(Directions.LEFT, default_size)) {
    				previous = actual;
	    			actual = Directions.LEFT;
	    			step = -10;
	    			return true;
	    		}
	    		return false;
    		}
    		else {
    			if(checkSpace(Directions.RIGHT, default_size)) {
    				previous = actual;
	    			actual = Directions.RIGHT;
	    			step = 10;
	    			return true;
    			}
    			return false;
    		}
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
    		d = new Vector3(0, 1, 0);
    	}
    	if(direction == Directions.DOWN) {
    		d = new Vector3(0, -1, 0);
    		sign = -1;
    	}
    	int i = 1;
    	while(i <= size) {
    		if(Physics.Raycast(new Vector3(x + 20 * sign, y + 20 * sign, 0), d, out hitInfo, 10f * i)) {
    			//Debug.Log("collision");
    			return false;
    		}
    		i++;
    	}
    	//Debug.Log("not collision");
    	return true;
    }


    private void putCorridor(int x, int y) {
    	putFloor(x, y);
    	GameObject w1 = (GameObject)GameObject.Instantiate(wall);
    	GameObject w2 = (GameObject)GameObject.Instantiate(wall);
    	if(actual == Directions.LEFT || actual == Directions.RIGHT) {
	    	if(direction_changed) {
	    		if(previous == Directions.DOWN) {
    					w1.transform.position = new Vector3(x, y - 5, 6);
    					w1.transform.eulerAngles = new Vector3(0, 0, 0);
    			} else if(previous == Directions.TOP) {
						w1.transform.position = new Vector3(x, y + 5, 6);
						w1.transform.eulerAngles = new Vector3(0, 0, 0);
    			}
    			if(actual == Directions.RIGHT) {
					w2.transform.position = new Vector3(x - 5, y, 6);
                    if(torch_created) {
                        putTorch(x - 5, y, new Vector3(90, 30, 0));
                    }
    			} else { 
    				w2.transform.position = new Vector3(x + 5, y, 6);
                    if(torch_created) {
                        putTorch(x + 5, y, new Vector3(90, -30, 0));
                    }
                }
    		} else {
    			if(torch_created) {
    				putTorch(x, y + 5, new Vector3(120, 0, 0));
    				putTorch(x, y - 5, new Vector3(60, 0, 0));
    			}
		    	w1.transform.position = new Vector3(x, y + 5, 6);
		    	w2.transform.position = new Vector3(x, y - 5, 6);
		    	w1.transform.eulerAngles = new Vector3(0, 0, 0);
		    	w2.transform.eulerAngles = new Vector3(0, 0, 0);	
    		}
    	} else {
    		if(direction_changed) {
				if(previous == Directions.RIGHT) {
					w1.transform.position = new Vector3(x + 5, y, 6);
				} else if(previous == Directions.LEFT) {
					w1.transform.position = new Vector3(x - 5, y, 6);
	    		}
	    		if(actual == Directions.DOWN) {
	    				w2.transform.position = new Vector3(x, y + 5, 6);
	    				w2.transform.eulerAngles = new Vector3(0, 0, 0);
                        if(torch_created) {
                            putTorch(x, y + 5, new Vector3(120, 0, 0));
                        }
	    		} else {
	    				w2.transform.position = new Vector3(x, y - 5, 6);
	    				w2.transform.eulerAngles = new Vector3(0, 0, 0);
                        if(torch_created) {
                            putTorch(x, y - 5, new Vector3(60, 0, 0));
                        }
	    		}
    		} else {
    			if(torch_created) {
    				putTorch(x + 5, y, new Vector3(90, -30, 0));
    				putTorch(x - 5, y, new Vector3(90, 30, 0));
    			}
		    	w1.transform.position = new Vector3(x + 5, y, 6);
		    	w2.transform.position = new Vector3(x - 5, y, 6);
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
    		putDoor(x - step / 2, y);
    	} else {
    		putDoor(x, y - step / 2);
    	}
    	int sign = step / Mathf.Abs(step);
    	for(int i = 0; Mathf.Abs(i) < height; i+= step) {
        	for(int j = 0; Mathf.Abs(j) < width; j+= step) {
        		putFloor(x + j, y + i);
        		if(j == 0) {
        			GameObject w = (GameObject)GameObject.Instantiate(wall);
	    			w.transform.position = new Vector3(x + j - 5 * sign, y + i, 6);
        		}
        		if (Mathf.Abs(j) == width - 10) {
        			GameObject w = (GameObject)GameObject.Instantiate(wall);
	    			w.transform.position = new Vector3(x + j + 5 * sign, y + i, 6);
        		} 
        		if (i == 0) {
        			GameObject w = (GameObject)GameObject.Instantiate(wall);
	    			w.transform.position = new Vector3(x + j, y + i - 5 * sign, 6);
	    			w.transform.eulerAngles = new Vector3(0, 0, 0);
        		} 
        		if (Mathf.Abs(i) == height - 10) {
        			GameObject w = (GameObject)GameObject.Instantiate(wall);
	    			w.transform.position = new Vector3(x + j, y + i + 5 * sign, 6);
	    			w.transform.eulerAngles = new Vector3(0, 0, 0);
        		}
        	}
        }


    	x += width * (step / Mathf.Abs(step));
    	y += height * (step / Mathf.Abs(step));
  
    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
    		y -= step;
    		putDoor(x - step / 2, y);
    	} else {
    		x -= step;
    		putDoor(x, y - step / 2);
    	}
    	return true;
    }

    private void putDoor(int x, int y) {
		Vector3 rotation;
    	if(actual == Directions.RIGHT || actual == Directions.LEFT)
    		rotation = new Vector3(0, 90, 90);
    	else
    		rotation = new Vector3(90, 0, 0);
    	GameObject d = (GameObject)GameObject.Instantiate(door);
    	d.transform.position = new Vector3(x, y, 10);
    	d.transform.eulerAngles = rotation;

    }

    private void putFloor(int x, int y) {
    	GameObject f = (GameObject)GameObject.Instantiate(floor);
    	f.transform.position = new Vector3(x, y, 0);
    }

    private void putTorch(int x, int y, Vector3 rotation) {
    	GameObject t = (GameObject)GameObject.Instantiate(torch);
    	t.transform.position = new Vector3(x, y, 5);
    	t.transform.eulerAngles = rotation;
    }

}