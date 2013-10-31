using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

	public GameObject wall;
	public GameObject floor;
	public GameObject door;
	public GameObject torch;
	public GameObject axe;
	public GameObject flameThrower;
    public GameObject shieldAndSword;
    public GameObject box;
    public GameObject barrel;
	public TimeController timeController;
	public ScoreManager scoreManager;
	private int width = 400;
	private int height = 400;
	private int x = 0;
	private int z = 0;
	private int seed = 0;
	private enum Directions {LEFT, RIGHT, TOP, DOWN};
	private Directions actual;
	private Directions previous;
	private int step = 10;
	private bool create = true;
	private double elapsedTime = 0;
	private bool room_created = false;
	private bool direction_changed = false;
	private bool torch_created = true;
    private int default_size = 5; //minimum size to consider in order to place items
    private bool start = true;
	private float roomAxeProbability = 0.05f;
	private float corridorAxeProbability = 0.2f;
	private bool difficult = false;
	
	void Start() {
		height = PlayerPrefs.GetInt("height");
		width = PlayerPrefs.GetInt("width");
		seed = PlayerPrefs.GetInt("seed");
		string diff = PlayerPrefs.GetString("difficult");
		if(seed == 0) {
			seed = (int)Random.value;
		}
		Random.seed = seed;
		Debug.Log(height);
		Debug.Log(width);
		Debug.Log(seed);
		Debug.Log(diff);
        ScoreManager.axesLeft = 0;
		difficult = bool.Parse (diff);
		if(difficult) {
			roomAxeProbability = 0.1f;
			corridorAxeProbability = 0.35f;
		}
		GenerateLevel(width, height, difficult);
	}
	
	public void GenerateLevel(int width, int height, bool difficulty) {
        putWall(x - 5, 6, z, new Vector3(0, 90, 0));
        CreateLevel(0, 0, Directions.RIGHT);
		timeController.SetTime();
	}

	void CreateLevel(int _x, int _z, Directions _actual) {
        x = _x;
        z = _z;
		actual = previous = _actual;
        create = true;
        if(actual == Directions.RIGHT || actual == Directions.TOP)
            step = 10;
        else 
            step = -10;
        while(create && Mathf.Abs(x) < width && Mathf.Abs(z) < height && elapsedTime < 5) {
        	elapsedTime += Time.deltaTime;
        	if(!checkSpace(x, z, actual, default_size)) {
        		if(!changeDirection()) {
        			create = false;
                    putExit(x, z, actual);
                    return;
                } else 
        			direction_changed = true;
        	}

    		if(Random.value > 0.75f) {
                if(createRoom())
                    return;
    		} else {
    			if(Random.value > 0.6f) {
                    if(changeDirection())
    				    direction_changed = true;
                }
            }
    		putCorridor(x, z);
    		if(!room_created) {
	    		if(actual == Directions.RIGHT || actual == Directions.LEFT) {
	    			x += step;
	    		} else {
	        		z += step;
	    		}
    		}
    		room_created = direction_changed = start = false;
        }
        if(Mathf.Abs(x) < width || Mathf.Abs(z) < height)
            putExit(x, z, actual);
    }

    private bool changeDirection() {
        if(!checkSpace(x, z, actual, 2))
            return false;
    	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
    		if(Random.value > 0.5f) {
    			if(checkSpace(x, z, Directions.DOWN, default_size)) {
                    putCorridor(x, z);
                    x += step;
    				previous = actual;
	   				actual = Directions.DOWN;
	   				step = -10;
	   			} else
	   		          return false;
   			} else {
   				if(checkSpace(x, z, Directions.TOP, default_size)) {
                    putCorridor(x, z);
                    x += step;
   					previous = actual;
	   				actual = Directions.TOP;
	   				step = 10;
   				} else
                    return false;
            }
        } else  {
    		if(Random.value > 0.5f) {
    			if(checkSpace(x, z, Directions.LEFT, default_size)) {
                    putCorridor(x, z);
                    z += step;
    				previous = actual;
	    			actual = Directions.LEFT;
	    			step = -10;
	    		} else
	    		     return false;
    		}
    		else {
    			if(checkSpace(x, z, Directions.RIGHT, default_size)) {
                    putCorridor(x, z);
                    z += step;
    				previous = actual;
	    			actual = Directions.RIGHT;
	    			step = 10;
    			} else
    			     return false;
    		}
        }
        return true;
    }

    private bool checkSpace(int x, int z, Directions direction, int size) {
        //Debug.Log("x:" + x + " z:" + z + " dir:" + direction + " size:" + size);
    	Vector3 d = new Vector3(0, 0, 0);
    	UnityEngine.RaycastHit hitInfo = new RaycastHit();
        if(x >= height || x < 0 || z >= width || z < 0)
            return false;
    	if(direction == Directions.LEFT) {
    		d = new Vector3(-1, 0, 0);
            if(x - size * 10 <= 0)
                return false;
    	}
    	if(direction == Directions.RIGHT) {
    		d = new Vector3(1, 0, 0);
            if(x + size * 10 >= width)
                return false;
    	}
    	if(direction == Directions.TOP) {
    		d = new Vector3(0, 0, 1);
            if(z + size * 10 >= height)
                return false;
    	}
    	if(direction == Directions.DOWN) {
    		d = new Vector3(0, 0, -1);
            if(z - size * 10 <= 0) {
                return false;
            }
    	}
        if(Physics.Raycast(new Vector3(x, 5, z), d, out hitInfo, 10f * size)) {
            //Debug.Log(hitInfo.collider.tag);
    			return false;
        }
    	return true;
    }


    private void putCorridor(int x, int z) {
    	putFloor(x, z);
		putAxe(x, z, corridorAxeProbability);
    	if(actual == Directions.LEFT || actual == Directions.RIGHT) {
	    	if(direction_changed) {
	    		if(previous == Directions.DOWN) {
                    putWall(x, 6, z - 5, new Vector3(0, 0, 0));
    			} else if(previous == Directions.TOP) {
                    putWall(x, 6, z + 5, new Vector3(0, 0, 0));
    			}
    			if(actual == Directions.RIGHT) {
                    putWall(x - 5, 6, z, new Vector3(0, 90, 0));
                    if(torch_created) {
                        putTorch(x - 5, z, new Vector3(0, 0, -30));
                    }
    			} else { 
                    putWall(x + 5, 6, z, new Vector3(0, 90, 0));
                    if(torch_created) {
                        putTorch(x + 5, z, new Vector3(0, 0, 30));
                    }
                }
    		} else {
    			if(torch_created) {
    				putTorch(x, z + 5, new Vector3(-30, 0, 0));
    				putTorch(x, z - 5, new Vector3(30, 0, 0));
    			} else {
                    putShieldAndSword(x, z + 4.5f, new Vector3(0, 90, 0));
                    putShieldAndSword(x, z - 4.5f, new Vector3(0, 270, 0));
                }
                putWall(x, 6, z + 5, new Vector3(0, 0, 0));
                putWall(x, 6, z - 5, new Vector3(0, 0, 0));
    		}
    	} else {
    		if(direction_changed) {
				if(previous == Directions.RIGHT) {
                    putWall(x + 5, 6, z, new Vector3(0, 90, 0));
				} else if(previous == Directions.LEFT) {
                    putWall(x - 5, 6, z, new Vector3(0, 90, 0));
	    		}
	    		if(actual == Directions.DOWN) {
                    putWall(x, 6, z + 5, new Vector3(0, 0, 0));
                    if(torch_created) {
                        putTorch(x, z + 5, new Vector3(-30, 0, 0));
                    }
	    		} else {
                    putWall(x, 6, z - 5, new Vector3(0, 0, 0));
                    if(torch_created) {
                        putTorch(x, z - 5, new Vector3(30, 0, 0));
                    }
	    		}
    		} else {
    			if(torch_created) {
    				putTorch(x + 5, z, new Vector3(0, 0, 30));
    				putTorch(x - 5, z, new Vector3(0, 0, -30));
    			} else {
                    putShieldAndSword(x + 4.5f, z, new Vector3(0, 180, 0));
                    putShieldAndSword(x - 4.5f, z, new Vector3(0, 0, 0));
                }
                putWall(x + 5, 6, z, new Vector3(0, 90, 0));
                putWall(x - 5, 6, z, new Vector3(0, 90, 0));
				putFlameThrower(x, z, 0.25f, new Vector3(1, 0, 0));
	    	}
    	}
    	torch_created = !torch_created;
    }

    private bool createRoom() {
        //tamaño de la habitacion
    	int width = ((int) (Random.value * 5) + 5) * 10;
    	int height = ((int) (Random.value * 5) + 5) * 10;

        int entrance_door_x = 0;
        int entrance_door_z = 0;
        int exit_door_x = 0;
        int exit_door_z = 0;
        int extra_door_x = 0;
        int extra_door_z = 0;
        int sign = step / Mathf.Abs(step);
        bool other_torch = true;
        bool extra_exit = false;
        bool exit_door = false;
        bool axe;
        Directions extra_direction = actual;
        int rand;

        //chequeos de espacios para ver si se puede poner o no
    	if(!checkSpace(x, z, actual, Mathf.Max(width, height) / 10 + 1))
    		return false;
    	if(actual == Directions.RIGHT){
            for(int i = 0; i < width / 10; i ++)
        		if(!checkSpace(x + i * step, z, Directions.TOP, height / 10 + 1))
        			return false;
    	}
    	if(actual == Directions.LEFT){
            for(int i = 0; i < width / 10; i ++)
        		if(!checkSpace(x + i * step, z, Directions.DOWN, height / 10 + 1)){
        			return false;
            }
    	}
    	if(actual == Directions.DOWN){
            for(int i = 0; i < height / 10; i ++)
        		if(!checkSpace(x, z + i * 10, Directions.LEFT, width / 10 + 1)){
        			return false;
            }
    	}
    	if(actual == Directions.TOP){
            for(int i = 0; i < height / 10; i ++)
        		if(!checkSpace(x, z + i * 10, Directions.RIGHT, width / 10 + 1))
        			return false;
    	}

        //pongo la puerta de salida en la direccion opuesta a la que entre, en una posicion random.
        if(actual == Directions.RIGHT || actual == Directions.LEFT) {
            if(!start) {
                putDoor(x - step / 2, z);
            }
            start = false;
            entrance_door_x = x - step / 2;
            entrance_door_z = z;

            rand = 2 + (int) (Random.value * (height/10 - 2));
            rand *= 10;
            if(checkSpace(x + width * sign - step / 2, z + rand * sign, actual, default_size)) {
                putDoor(x + width * sign - step / 2, z + rand * sign); //salida
                exit_door_x = x + width * sign - step / 2;
                exit_door_z = z + rand * sign;
                exit_door = true;
            }

            if(Random.value > 0.3) { //pongo la puerta extra?
                if(Random.value > 0.5) { //ok, donde?
                    rand = 2 + (int) (Random.value * (width/10 - 2));
                    rand *= 10;
                    if(checkSpace(x - rand * sign + width * sign, z - (step / 2) + height * sign, Directions.TOP, default_size) && difficult) {
                        putDoor(x - rand * sign + width * sign, z - (step / 2) + height * sign, new Vector3(90, 0, 0)); //arriba
                        extra_exit = true;
                        extra_door_x = x - rand * sign + width * sign;
                        extra_door_z = z - (step / 2) + height * sign;
                        if(actual == Directions.RIGHT)
                            extra_direction = Directions.TOP;
                        else
                           extra_direction = Directions.DOWN;
                        Debug.Log("arriba");
                    }
                }
                else {
                    rand = 2 + (int) (Random.value * (width/10 - 2));
                    rand *= 10;
                    if(checkSpace(x + rand * sign, z - step / 2, Directions.DOWN, default_size) && difficult) {
                        putDoor(x + rand * sign, z - step / 2, new Vector3(90, 0, 0) ); //abajo
                        Debug.Log("abajo");
                        extra_exit = true;
                        extra_door_x = x + rand * sign;
                        extra_door_z = z - step / 2;
                        if(actual == Directions.RIGHT)
                            extra_direction = Directions.DOWN;
                        else
                           extra_direction = Directions.TOP;
                    }
                }
            }

        } else {
            if(!start) {
                putDoor(x, z - step / 2);
            }
            start = false;
            entrance_door_x = x;
            entrance_door_z = z - step / 2;

            rand = 2 + (int) (Random.value * (width/10 - 2));
            rand *= 10;
            if(checkSpace(x + rand * sign, z + height * sign - step / 2, actual, default_size)) {
                putDoor(x + rand * sign, z + height * sign - step / 2); //salida
                exit_door_x = x + rand * sign;
                exit_door_z = z + height * sign - step / 2;
                exit_door = true;
            }

            if(Random.value > 0.5) { 
                if(Random.value > 0.5) {
                    rand = 2 + (int) (Random.value * (height/10 - 2));
                    rand *= 10;
                    if(checkSpace(x - step / 2 + width * sign, z + rand * sign, Directions.RIGHT, default_size) && difficult) {
                        putDoor(x - step / 2 + width * sign, z + rand * sign, new Vector3(90, 90, 0)); //derecha
                        Debug.Log("derecha");
                        extra_exit = true;
                        extra_door_x = x - step / 2 + width * sign;
                        extra_door_z = z + rand * sign;
                        if(actual == Directions.TOP)
                            extra_direction = Directions.RIGHT;
                        else
                           extra_direction = Directions.LEFT;
                    }
                }
                else {
                    rand = 2 + (int) (Random.value * (height/10 - 2));
                    rand *= 10;
                    if(checkSpace(x - step / 2, z + rand * sign, Directions.LEFT, default_size) && difficult) {
                        putDoor(x - step / 2, z + rand * sign, new Vector3(90, 90, 0)); //izquierda
                        Debug.Log("izquierda");
                        extra_exit = true;
                        extra_door_x = x - step / 2;
                        extra_door_z = z + rand * sign;
                        if(actual == Directions.TOP)
                            extra_direction = Directions.LEFT;
                        else
                           extra_direction = Directions.RIGHT;
                    }
                }
            }
        }

    	for(int i = 0; Mathf.Abs(i) < height; i+= step) {
        	for(int j = 0; Mathf.Abs(j) < width; j+= step) {
        		putFloor(x + j, z + i);
				axe = putAxe(x + j, z + i, roomAxeProbability);
        		if(j == 0 && !((z + i) == entrance_door_z && (x + j - 5 * sign) == entrance_door_x) && !(exit_door && (z + i) == exit_door_z && (x + j - 5 * sign) == exit_door_x) && !(extra_exit && (z + i) == extra_door_z && (x + j - 5 * sign) == extra_door_x)) {
                    putWall(x + j - 5 * sign, 6, z + i, new Vector3(0, 90, 0));
                    if(other_torch) {
                        if(actual == Directions.RIGHT || actual == Directions.TOP) {
                            putTorch(x + j - 5 * sign, z + i, new Vector3(0, 0, -30));
                            //putFlameThrower(x + j, z + i, 0.4f, new Vector3(1, 0, 0));
						} else {
                            putTorch(x + j - 5 * sign, z + i, new Vector3(0, 0, 30));
						}
                    } else if(!axe && !(entrance_door_x == (x + j - 5 * sign) && entrance_door_z == (z + i)) && i != 0 && i != height - 10) {
                        if( !(entrance_door_x == (x + j) && entrance_door_z == (z + i - 5 * sign)) )
                            putFurniture(x + j, z + i);
                    }
                }
                if (Mathf.Abs(j) == (width - 10) && !((z + i) == entrance_door_z && (x + j + 5 * sign) == entrance_door_x) && !(exit_door && (z + i) == exit_door_z && (x + j + 5 * sign) == exit_door_x) && !(extra_exit && (z + i) == extra_door_z && (x + j + 5 * sign) == extra_door_x)) {
                    putWall(x + j + 5 * sign, 6, z + i, new Vector3(0, 90, 0));
                    if(!other_torch) {
                        if(actual == Directions.RIGHT || actual == Directions.TOP) {
                            putTorch(x + j + 5 * sign, z + i, new Vector3(0, 0, 30));
						} else {
                            putTorch(x + j + 5 * sign, z + i, new Vector3(0, 0, -30));
                            //putFlameThrower(x + j, z + i, 0.4f, new Vector3(1, 0, 0));
						}
                    } else if(!axe && i != 0 && i != height - 10)
                            putFurniture(x + j, z + i);
                    other_torch = !other_torch;
                } 
                if (i == 0 && ((x + j) != entrance_door_x || (z + i - 5 * sign) != entrance_door_z) && (exit_door && (x + j) != exit_door_x || (z + i - 5 * sign) != exit_door_z) && !(extra_exit && (z + i - 5 * sign) == extra_door_z && (x + j) == extra_door_x)) {
                    putWall(x + j, 6, z + i - 5 * sign, new Vector3(0, 0, 0));
                    if(torch_created) {
                        if(actual == Directions.RIGHT || actual == Directions.TOP) {
                            putTorch(x + j, z + i - 5 * sign, new Vector3(30, 0, 0));
                            putFlameThrower(x + j, z + i, 0.3f, new Vector3(0, 0, 1));
						} else {
                            putTorch(x + j, z + i - 5 * sign, new Vector3(-30, 0, 0));
						}
                    } else if(!axe && !(entrance_door_x == (x + j - 5 * sign) && entrance_door_z == (z + i)) && j != 0 && j != width - 10) {
                            putFurniture(x + j, z + i);
                    }
                    torch_created = !torch_created;
                } 
                if (Mathf.Abs(i) == height - 10 && ((x + j) != entrance_door_x || (z + i + 5 * sign) != entrance_door_z) && (exit_door && (x + j) != exit_door_x || (z + i + 5 * sign) != exit_door_z) && !(extra_exit && (z + i + 5 * sign) == extra_door_z && (x + j) == extra_door_x)) {
                    putWall(x + j, 6, z + i + 5 * sign, new Vector3(0, 0, 0));
                    if(torch_created) {
                        if(actual == Directions.RIGHT || actual == Directions.TOP) {
                            putTorch(x + j, z + i + 5 * sign, new Vector3(-30, 0, 0));
						} else {
                            putTorch(x + j, z + i + 5 * sign, new Vector3(30, 0, 0));
                            putFlameThrower(x + j, z + i, 0.3f, new Vector3(0, 0, 1));
						}
                    } else if(!axe && j != 0 && j != width - 10) {
                            putFurniture(x + j, z + i);
                    }
                    torch_created = !torch_created;
        		}
        	}
        }

    	x += width * (step / Mathf.Abs(step));
    	z += height * (step / Mathf.Abs(step));
  
        if(exit_door) {
        	if(actual == Directions.RIGHT || actual == Directions.LEFT) {
        		z -= step;
                CreateLevel(exit_door_x + step / 2, exit_door_z, actual);
             } else {
                x -= step;   
                CreateLevel(exit_door_x, exit_door_z + step / 2, actual);
            }
        }

        if(extra_exit) {
            if(extra_direction == Directions.RIGHT)
                CreateLevel(extra_door_x + 10 / 2, extra_door_z, extra_direction);
             if(extra_direction == Directions.LEFT)
                CreateLevel(extra_door_x - 10 / 2, extra_door_z, extra_direction);
            if(extra_direction == Directions.TOP) 
                CreateLevel(extra_door_x, extra_door_z + 10 / 2, extra_direction);
            if(extra_direction == Directions.DOWN)
                CreateLevel(extra_door_x, extra_door_z - 10 / 2, extra_direction);
        }
        extra_exit = false;
    	return true;
    }

    private void putFurniture(float x, float z) {
        GameObject b;
        double rand = Random.value;
        if(rand > 0.6f)
            return;
        if(rand > 0.3f)
            b = (GameObject)GameObject.Instantiate(barrel);
        else
            b = (GameObject)GameObject.Instantiate(box);
        b.transform.position = new Vector3(x, 0, z);
    }

    private void putShieldAndSword(float x, float z, Vector3 rotation) {
        GameObject s = (GameObject)GameObject.Instantiate(shieldAndSword);
        s.transform.position = new Vector3(x, 10, z);
        s.transform.eulerAngles = rotation;
    }
	
	private bool putAxe(int x, int z, float probability) {
		if(Random.value < probability && !(x == 0 && z == 0)) { //no puedo poner un hacha en el lugar de inicio
			GameObject d = (GameObject)GameObject.Instantiate(axe);
		    d.transform.position = new Vector3(x, 4f, z);
			d.transform.eulerAngles = new Vector3(-90, 0, 0);
            ScoreManager.incrementAxeCount();
            return true;
		}
        return false;
	}
	
	private void putFlameThrower(int x, int z, float probability, Vector3 dir) {
		if(Random.value < probability) {
			GameObject f = (GameObject)GameObject.Instantiate(flameThrower);
		    f.transform.position = new Vector3(x, 5, z);
            f.transform.eulerAngles = dir;
		}
	}
	
    private void putDoor(int x, int z) {
		Vector3 rotation;
    	if(actual == Directions.RIGHT || actual == Directions.LEFT)
    		rotation = new Vector3(90, 90, 0);
    	else
    		rotation = new Vector3(90, 0, 0);
    	GameObject d = (GameObject)GameObject.Instantiate(door);
    	d.transform.position = new Vector3(x, 7.5f, z);
    	d.transform.eulerAngles = rotation;
    }

    private void putDoor(int x, int z, Vector3 rotation) {
        GameObject d = (GameObject)GameObject.Instantiate(door);
        d.transform.position = new Vector3(x, 7.5f, z);
        d.transform.eulerAngles = rotation;
    }

    private void putFloor(int x, int z) {
    	GameObject f = (GameObject)GameObject.Instantiate(floor);
    	f.transform.position = new Vector3(x, 0, z);
    }

    private void putTorch(int x, int z, Vector3 rotation) {
    	GameObject t = (GameObject)GameObject.Instantiate(torch);
    	t.transform.position = new Vector3(x, 7, z);
    	t.transform.eulerAngles = rotation;
    }

    private void putWall(int x, int y, int z, Vector3 rotation) {
        GameObject w = (GameObject)GameObject.Instantiate(wall);
        w.transform.position = new Vector3(x, 7.5f, z);
        w.transform.eulerAngles = rotation;   
    }

   private void putExit(int x, int z, Directions dir) {
        if(dir == Directions.RIGHT || dir == Directions.LEFT)
            putWall(x - step / 2, 6, z, new Vector3(0, 90, 0));
        if(dir == Directions.TOP || dir == Directions.DOWN)
            putWall(x, 6, z - step / 2, new Vector3(0, 0, 0));
    }

}