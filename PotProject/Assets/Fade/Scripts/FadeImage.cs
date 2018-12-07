using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeImage : UnityEngine.UI.Graphic , IFade
{
	[SerializeField]
	private Texture maskTexture = null;

	[SerializeField, Range (0, 1)]
	private float cutoutRange;

	public float Range {
		get {
			return cutoutRange;
		}
		set {
			cutoutRange = value;
			UpdateMaskCutout (cutoutRange);
		}
	}

	protected override void Start ()
	{
		base.Start ();
		UpdateMaskTexture (maskTexture);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(test(1));
        }

        gameObject.transform.Find("test");
    }

    IEnumerator test(float interval)
    {
        float time = 0;
        while (time <= interval)
        {
            cutoutRange = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.unscaledDeltaTime;
            UpdateMaskCutout(cutoutRange);
            yield return 0;
        }

        yield return new WaitForSeconds(0.5f);

        //だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            cutoutRange = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.unscaledDeltaTime;
            UpdateMaskCutout(cutoutRange);
            yield return 0;
        }
    }

    private void UpdateMaskCutout (float range)
	{
		enabled = true;
		material.SetFloat ("_Range", 1 - range);

		//if (range <= 0) {
		//	this.enabled = false;
		//}
	}

	public void UpdateMaskTexture (Texture texture)
	{
		material.SetTexture ("_MaskTex", texture);
		material.SetColor ("_Color", color);
	}

	#if UNITY_EDITOR
	protected override void OnValidate ()
	{
		base.OnValidate ();
		UpdateMaskCutout (Range);
		UpdateMaskTexture (maskTexture);
	}
	#endif
}
