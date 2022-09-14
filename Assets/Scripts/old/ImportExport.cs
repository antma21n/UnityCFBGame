using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportExport : MonoBehaviour
{
	private string pdfFileType;

	void Start()
	{
		pdfFileType = NativeFilePicker.ConvertExtensionToFileType("txt"); // Returns "application/pdf" on Android and "com.adobe.pdf" on iOS
		Debug.Log("pdf's MIME/UTI is: " + pdfFileType);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			// Don't attempt to import/export files if the file picker is already open
			if (NativeFilePicker.IsFilePickerBusy())
				return;
			/* */
			if (Input.mousePosition.x < Screen.width / 3)
			{
				// Pick a PDF file
				NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
				{
					if (path == null)
						Debug.Log("Operation cancelled");
					else
						Debug.Log("Picked file: " + path);
				}, new string[] { pdfFileType });

				Debug.Log("Permission result: " + permission);
			}
			/* */
			else if (Input.mousePosition.x < Screen.width * 2 / 3)
			{
#if UNITY_ANDROID
			// Use MIMEs on Android
			string[] fileTypes = new string[] { "image/*", "video/*" };
#else
				// Use UTIs on iOS
				string[] fileTypes = new string[] { "public.image", "public.movie" };
#endif

				// Pick image(s) and/or video(s)
				NativeFilePicker.Permission permission = NativeFilePicker.PickMultipleFiles((paths) =>
				{
					if (paths == null)
						Debug.Log("Operation cancelled");
					else
					{
						for (int i = 0; i < paths.Length; i++)
							Debug.Log("Picked file: " + paths[i]);
					}
				}, fileTypes);

				Debug.Log("Permission result: " + permission);
			}
			else
			{
				// Create a dummy text file
				string filePath = System.IO.Path.Combine(Application.temporaryCachePath, "test.txt");
				System.IO.File.WriteAllText(filePath, "Hello world!");

				// Export the file
				NativeFilePicker.Permission permission = NativeFilePicker.ExportFile(filePath, (success) => Debug.Log("File exported: " + success));

				Debug.Log("Permission result: " + permission);
			}
		}
	}
}
