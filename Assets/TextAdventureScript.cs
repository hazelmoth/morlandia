using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAdventureScript : MonoBehaviour 
{
	private string descStartVillage;
	private string descField;
	private string descHill;
	private string descForest;
	private string descCave;
	private string descShop;
	private string descEvilLair;

	private string descCaveFront;
	private string descCaveBack;

	private string descShopInterior;

	private string entrance;

	public string characterName;
	private string upperCharacterName;


	public string[,] matrix;
	public string[,] grid;
	public string[,] cavegrid;
	public string[,] shopgrid;
	public string[] randomSentences;
	public int x;
	public int y;
	public int lastX;
	public int lastY;
	public int introSentence;
	public int endSentence;
	public bool inIntro;
	public bool inEvilLair;
	public bool hasName;
	public bool saidYes;
	public bool saidNo;
	public string[] introSentences;
	public string[] endSentences;

	public Text descriptionText;
	public Text descriptionText2;
	public InputField inputField;
	public GameObject inputParent;

	void Start ()
	{
		descStartVillage = "You stand amoung the smoking remains of Wellspring, once your home.";
		descField = "You are standing in a field.";
		descHill = "You are standing on a hill.";
		descForest = "You are in a dense forest.";
		descCave = "You are standing by the mouth of a cave.";
		descShop = "You are standing next to a small shop.";
		descEvilLair = "You come before a monstrous tower jutting from the landscape, twisted and black as night. It can only be the lair of the Unknown One! The door gapes before you ominously.";

		descCaveFront = "You are inside the cave. The entrance is behind you to the north. The cave goes down deeper to the south.";
		descCaveBack = "You reach the end of the cave and find nothing. Well done.";

		descShopInterior = "You are in a small shop. The entrance is behind you to the north. There is absolutely nothing inside.";

		randomSentences = new string[] 
		{
					"You can feel a gentle breeze coming through.",
					"It is raining lightly.",
					"A sudden thunderstorm is rumbling violently.",
					"The sun is hot on your skin.",
					"The wind is picking up.",
					"The sky is a pleasant blue.",
					"Clouds are beginning to roll overhead.",
					"You can hear galgrons croaking in the distance.",
					"A galgron watches you nervously from a nearby tree.",
					"Wandering through the world, you can't help but be impressed with the developers.",
					"You try to stay focused on your goal of finding the Unknown One, on the east coast.",
					"You stop for a moment to mourn your dead parents.",
					"You inadvertently step on a flower, making you sad.",
					"You happen to recall that the Unknown One lives on the east coast.",
					"You see galgron tracks in the mud.",
					"You feel certain that east is the way to go.",
					"You suddenly miss your days of playing Kerbal Space Program back on the farm.",
					"You contemplate what a small world it is.",
					"You wish you had some cake to pass the time.",
					"Bored, you recite \"The Raven\" by Edgar Allen Poe.",
					"You find yourself wondering why Visual Basic exists.",
					"A flurry of snow rains down suddenly.",
					"You notice a sign describing the trafficking of hobbits to Isenguard.",
					"You pass by a wizard who seems insistent on preventing you from doing so.",
					"You consider that generally people do not simply walk into the Unknown One's Lair.",
					"You consider that what you're doing might be morally okay.",
					"You hear the gentle sound of a creek nearby.",
					"You roll your mushrooms around in your hand, lost in thought.",
					"You reminisce about the old days of prohibition.",
					"A wild galgron suddenly rushes by, startling you.",
					"A moderate earthquake catches you off guard.",
					"The grass reminds you off the old days of farming grass back in Wellspring.",
					"The pollen in the air brings a tear to your eye.",
					"You entertain the idea of starting a galgron vet.",
					"You briefly wonder what you did with your childhood.",
					"A bolt of lightning strikes dangerously close, jolting you out of thought.",
					"The calming scent of burning mutton is in the air.",
					"You dab your shoulderblade with a microfiber cloth.",
					"You have a bad feeling about this.",
					"You recieve a snap, but choose to ignore it.",
					"You wonder how much respect the Ink Spots actually had.",
					"You wonder about David Bowie's gender identity.",
					"You find yourself longing for a good boar hunt.",
					"You find yourself wondering why keyboardists are as respected as pianists.",
					"You consider whether procedural generation could be the future.",
					"A dust storm rumbles in the distance."
		};

		//Screen.lockCursor = true;

		inIntro = true;
		inEvilLair = false;
		hasName = false;

		introSentence = 0;
		endSentence = 0;



		/* Create a world grid and randomly select cave location */



		matrix = new string[50, 20];

		for (int i = 1; i <= 15; i++) {
			matrix [Random.Range (0, matrix.GetLength (0) - 1), Random.Range (0, matrix.GetLength (1) - 1)] = "shop";
		}
		for (int i = 1; i <= 15; i++) {
			matrix [Random.Range (0, matrix.GetLength (0) - 1), Random.Range (0, matrix.GetLength (1) - 1)] = "cave";
		}

		matrix[0, 12] = "startVillage";
		matrix[49, 6] = "evilLair";

		cavegrid = new string[1, 2];
		cavegrid[0, 0] = "caveFront";
		cavegrid[0, 1] = "caveBack";

		shopgrid = new string[1, 1];
        cavegrid[0, 0] = "shopInterior";

		/* Randomly select either a hill or field for each grid location */

		for (int mx = 0; mx < matrix.GetLength(0); mx++)
		{
			for (int my = 0; my < matrix.GetLength(1); my++)
			{
				if (matrix[mx, my] == null)
				{
					switch (Random.Range(1, 4))
					{
					case 1:
						matrix[mx, my] = "hill"; break;
					case 2:
						matrix[mx, my] = "field"; break;
					case 3:
						matrix[mx, my] = "forest"; break;
					}
				}
			}
		}

		/* Randomly choose start position */

		x = 0;
		y = 12;
		grid = matrix;


		inputField.ActivateInputField();

		/* Print the description of the start and set the second line to blank */

		UpdateDescription();
	}

	void Update ()
	{
		if (hasName == false) {
			descriptionText2.text = "";
			descriptionText.text = "Name your character:";
			if (Input.GetKeyDown (KeyCode.Return) == true && inputField.text != "") {
				characterName = inputField.text;
				upperCharacterName = characterName.ToUpper ();

				hasName = true;

				introSentences = new string[] {
					"You, " + characterName + ", were once a mere farmboy, working in the small village of Wellspring.",
					"Each day you labored on your parents' farm, earning your sustainance through the sweat of your brow.",
					"That is, until one fateful day.",
					"Formless, faceless ghouls swept across Wellspring in a cloud darker than night.",
					"The village was macerated into ash, every one of its inhabitants slaughtered.",
					"Every one, that is, but you, " + characterName + ".",
					"Having been out gathering mushrooms, you arrived just in time to watch the formless demons sweep away.",
					"As they left you heard whispers of a name that you could never forget:",
					"The Unknown One.",
					"At that moment you vowed that this evil would be destroyed at any cost.",
					"With nothing but your mushrooms, you turned to the East, to pursue the demons and find their leader.",
					"Such begins your journey."
				};

				endSentences = new string[] {
					"Cautiously, you step inside the pitch-black tower. A gnarly stone staircase runs upward into the darkness.",
					"Slowly, you ascend the steps, one at a time. The air becomes cold as you near the top.",
					"\"" + upperCharacterName + "!\"",
					"\"I know why you have come.\"",
					"\"I am truly impressed that you have made it this far. Perhaps I have misjudged you, " + characterName + ".\"",
					"\"As such I am offering you one final opportunity to spare your life.\"",
					"\"Join me, and the world will come to fear the name of " + characterName + ".\"",
				};

				descriptionText2.text = "(any key to continue)";
				inputParent.SetActive (false);
			}
		} else if (inIntro == true) {
			descriptionText.text = introSentences [introSentence];
			if (Input.anyKeyDown == true) {
				introSentence++;
			}
			if (introSentence > (introSentences.GetLength (0) - 1)) {
				inIntro = false;
				inputParent.SetActive (true);
				inputField.ActivateInputField ();
				UpdateDescription ();
				descriptionText2.text = "";
				inputField.text = "";
			}
		} else if (inEvilLair == true) {
			if (endSentence <= (endSentences.GetLength (0) - 1)) {
				descriptionText.text = endSentences [endSentence];
			}
			if (Input.anyKeyDown == true) {
				endSentence++;
			}
			if (endSentence == (endSentences.GetLength (0))) {
				inputParent.SetActive (true);
				inputField.text = "";
				inputField.ActivateInputField ();
				endSentence++;
			}
			if (endSentence > (endSentences.GetLength (0) - 1)) {

				descriptionText.text = "Do you join the Unknown One?";
				descriptionText2.text = "( Y / N )";
				if (Input.GetKeyDown (KeyCode.Return) == true) {
				string choice = inputField.text;
				choice = choice.ToLower();
					if (choice == "yes" || choice == "y") {
						saidYes = true;
						inEvilLair = false;
						inputParent.SetActive (false);
					} else if (choice == "no" || choice == "n") {
						saidNo = true;
						inEvilLair = false;
						inputParent.SetActive (false);
					}
				}

			}
		} else if (saidYes == true) 
		{
			descriptionText.text = characterName + " went on to become the most ruthlessly powerful entity the land of Morlandia had ever seen. Crushing villages for fun, he lived happily ever after.";
			descriptionText2.text = "(any key to exit game)";
			if (Input.anyKeyDown == true) 
			{
				QuitGame();
			}
		} 
		else if (saidNo == true) 
		{
			descriptionText.text = "Appalled, The Unknown One immediately extinguished " + characterName + ", crushing his body and dooming his soul to enslavement. And thus, your story ends.";
			descriptionText2.text = "(any key to exit game)";
			if (Input.anyKeyDown == true) 
			{
				QuitGame();
			}
		}

		else if (Input.GetKeyDown (KeyCode.Return) == true) 
		{
			inputField.ActivateInputField();
			ActivateTurn();
			UpdateDescription();
		}
	}

	public void QuitGame()
	{
		Screen.lockCursor = false;
		SceneManager.LoadSceneAsync ("Menu");
	}

	public void QuitApp()
	{
		Application.Quit();
	}

	public void Restart()
	{
		SceneManager.LoadSceneAsync ("Main");
	}



	public void UpdateDescription ()
	{

		/* Write out the description for the current area */

		Debug.Log ("UpdateDescription called");
		switch (grid [x, y]) {
		case "field":
			descriptionText.text = descField;
			if (Random.Range (1, 3) == 1 && descriptionText2.text == "") {
				descriptionText2.text = randomSentences [Random.Range (0, ((randomSentences.Length) - 1))];
			}
			entrance = null;
			break;
		case "hill":
			descriptionText.text = descHill;
			if (Random.Range (1, 3) == 1 && descriptionText2.text == "") {
				descriptionText2.text = randomSentences [Random.Range (0, randomSentences.Length)];
			}
			entrance = null;
			break;
			case "forest":
			descriptionText.text = descForest;
			if (Random.Range (1, 3) == 1 && descriptionText2.text == "") {
				descriptionText2.text = randomSentences [Random.Range (0, randomSentences.Length)];
			}
			entrance = null;
			break;
		case "startVillage":
			descriptionText.text = descStartVillage;
			entrance = null;
			break;
		case "cave":
			descriptionText.text = descCave;
			entrance = "cave";
			break;
		case "shop":
			descriptionText.text = descShop;
			entrance = "shop";
			break;
		case "evilLair":
			descriptionText.text = descEvilLair;
			entrance = "evilLair";
			break;
		case "caveFront":
			descriptionText.text = descCaveFront;
			entrance = null;
			break;
		case "caveBack":
			descriptionText.text = descCaveBack;
			break;
		}
	}





	public void ActivateTurn ()
	{

			string command = inputField.text;
			command = command.ToLower();
			descriptionText2.text = "";

		if (entrance == "cave")
                {
                    switch (command.ToLower())
                    {
                        case "enter":
                        case "enter cave":
                        case "cave":
                        case "go inside":
                        case "go inside cave":
                            grid = cavegrid;
                            lastX = x;
                            lastY = y;
                            x = 0;
                            y = 0;
							descriptionText.text = descCaveFront;
							entrance = null;
                            UpdateDescription();
							inputField.text = "";
							inputField.ActivateInputField();
                            return;
                    }
                }

                if (entrance == "shop")
                {
                    switch (command.ToLower())
                    {
                        case "enter":
                        case "enter shop":
                        case "shop":
						case "go inside":
          	            case "go inside shop":
                            grid = shopgrid;
                            lastX = x;
                            lastY = y;
                            x = 0;
                            y = 0;
							descriptionText.text = descShopInterior;
							entrance = null;
                            UpdateDescription();
							inputField.text = "";
							inputField.ActivateInputField();
                            return;
                    }
                }

			if (entrance == "evilLair")
                {
                    switch (command.ToLower())
                    {
                        case "enter":
                        case "enter lair":
                        case "enter tower":
                        case "go inside":
                        case "go inside lair":
                        case "go inside the lair":
                        case "enter the lair":
						case "go inside tower":
                        case "go inside the tower":
                        case "enter the tower":
                        case "proceed":
                            inEvilLair = true;
                            descriptionText2.text = "(any key to continue)";
                            inputParent.SetActive(false);
                            return;
                    }
                }

			if (grid == matrix)
                {
                    switch (command.ToLower())
                    {
                        case "north":
                        case "go north":
                        case "n":
                            if (y == 0)
                            { descriptionText2.text = ("You can go no further north, having reached the edge of the frigid, ice-covered Northern sea."); break; }
                            y--; break;
                        case "south":
                        case "go south":
                        case "s":
                            if (y == matrix.GetLength(1) - 1)
                            { descriptionText2.text = ("Your path to the south is cut off by the calm waters of the southern ocean."); break; }
                            y++; break;
                        case "west":
                        case "go west":
                        case "w":
                            if (x == 0)
                            { descriptionText2.text = ("To the west is the vast blue ocean, stretching as far as they eye can see."); break; }
                            x--; break;
                        case "east":
                        case "go east":
                        case "e":
                            if (x == matrix.GetLength(0) - 1)
                            { descriptionText2.text = ("The land ends abruptly at the treacherous Eastern Sea, infamous for its deadly currents."); break; }
                            x++; break;
                        case "jump":
                        	descriptionText2.text = "You launch yourself into the air with well-toned jumping skills.";
                            break;
						case "die":
						case "suicide":
						case "kill self":
                        	QuitGame();
                            break;
						case "cry":
						case "mourn":
                        	descriptionText2.text = "You break into uncontrollable sobbing.";
                            break;
                        default:
                            descriptionText2.text = ("You don't know how to " + command + ".");
                            break;
                    }
                }
                else if (grid == cavegrid)
                {
                    switch (command.ToLower())
                    {
                        case "north":
                        case "go north":
                        case "n":
                            if (y == 0)
                            {
                                x = lastX;
                                y = lastY;
                                grid = matrix;
                                entrance = "nothing";
                                break;
                            }
                            y--; UpdateDescription(); break;
                        case "south":
                        case "go south":
                        case "s":
                            if (y == 1)
                            { descriptionText2.text = ("The cave walls block your path to the south."); break; }
                            y++; break;
                        case "west":
                        case "go west":
                        case "w":
                            if (x == 0)
                            { descriptionText2.text = ("The cave walls block your path to the west."); break; }
                            x--; break;
                        case "east":
                        case "go east":
                        case "e":
                            if (x == 0)
                            { descriptionText2.text = ("The cave walls block your path to the east."); break; }
                            x++; break;
                        default:
                            descriptionText2.text = ("You don't know how to do that.");
                            break;
                    }
                }
                else if (grid == shopgrid)
                {
                    switch (command.ToLower())
                    {
                        case "north":
                        case "go north":
                        case "n":
                            if (y == 0)
                            {
                                x = lastX;
                                y = lastY;
                                grid = matrix;
                                entrance = "nothing";
                                break;
                            }
                            break;
                        case "south":
                        case "go south":
                        case "s":
                        case "west":
                        case "go west":
                        case "w":
                        case "east":
                        case "go east":
                        case "e":
                            descriptionText2.text = ("You can't do that here!");
                            break;
                    }
                }

                UpdateDescription();
                inputField.text = "";
				inputField.ActivateInputField();
	}

}