using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Text")]
public class TextStringSO : ScriptableObject
{
	[TextArea]
	public string Text;
}
