using UnityEngine;

namespace SORCE.Extensions
{
    public static class E_PlayfieldObject
	{
		/// <summary>
		/// Ensures equal numbers of buttons, buttonsExtra and buttonsPrices 
		/// </summary>
		/// <param name="playfieldObject">instance of the playfieldObject to normalize buttons on</param>
		public static void NormalizeButtons(this PlayfieldObject playfieldObject)
		{
			int buttonCount = playfieldObject.buttons.Count;
			int buttonExtraCount = playfieldObject.buttonsExtra.Count;
			int buttonPriceCount = playfieldObject.buttonPrices.Count;
			int buttonTotal = Mathf.Max(buttonCount, buttonExtraCount, buttonPriceCount);

			for (int i = buttonCount; i < buttonTotal; i++)
				playfieldObject.buttons.Add(null);

			for (int i = buttonExtraCount; i < buttonTotal; i++)
				playfieldObject.buttonsExtra.Add(null);

			for (int i = buttonPriceCount; i < buttonTotal; i++)
				playfieldObject.buttonPrices.Add(0);
		}

		public static void AddButton(this PlayfieldObject playfieldObject, string text, string extraText = null, int price = 0)
		{
			NormalizeButtons(playfieldObject);
			playfieldObject.buttons.Add(text);
			playfieldObject.buttonsExtra.Add(extraText);
			playfieldObject.buttonPrices.Add(price);
		}

		public static int RemoveButton(this PlayfieldObject playfieldObject, string text)
		{
			NormalizeButtons(playfieldObject);
			int removals = 0;

			for (int i = playfieldObject.buttons.Count - 1; i >= 0; i--)
				if (playfieldObject.buttons[i] == text)
				{
					playfieldObject.buttons.RemoveAt(i);
					playfieldObject.buttonsExtra.RemoveAt(i);
					playfieldObject.buttonPrices.RemoveAt(i);
					removals++;
				}

			return removals;
		}
	}
}
