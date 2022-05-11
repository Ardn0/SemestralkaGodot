using Godot;

namespace Semestralka.Save;

public class Save
{
	private string soubor = "user://score.save";
	private int maxSkore = 0;

	public void SaveGame(string skore)
	{
		if (maxSkore < int.Parse(skore))
		{
			File file = new File();
			file.Open(soubor, File.ModeFlags.Write);
			file.StoreString(skore);
			file.Close();
		}
	}

	public int LoadGame()
	{
		File file = new File();

		if (file.FileExists(soubor))
		{
			file.Open(soubor, File.ModeFlags.Read);
			maxSkore = int.Parse(file.GetAsText());
			file.Close();
		}

		return maxSkore;
	}
}
