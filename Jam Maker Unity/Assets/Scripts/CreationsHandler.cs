using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The goal of this file is to store an array of all possible creations given a file. Also to store player data and more.
//Should probably use a JSON file for this so its easier to represent all the information in a file.
//Could make storing playerprefs easier too.
//Will likely be reference by the progress UI and the RecipeHandler


public class CreationsHandler : MonoBehaviour
{
    Creation[] allCreations;
}
