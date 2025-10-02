using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UISample {
    public class Sample : VisualElement {
	public new class UxmlFactory : UxmlFactory<Sample> {}

	public Sample() {
	    var treeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/sample.uxml");
	    var container = treeAsset.Instantiate();
	    hierarchy.Add(container);

	    var samp = container.Q<VisualElement>("Root");
	    samp.AddManipulator(new Clickable(() => Debug.Log("Click")));
	}
    }
}
