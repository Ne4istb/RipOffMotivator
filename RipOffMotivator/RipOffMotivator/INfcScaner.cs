using System;
using System.Collections;

using NdefLibrary.Ndef;


namespace RipOffMotivator
{
	public interface INfcScaner
	{
		/// <summary>
		/// Gets if the device is able to detect NFC tags
		/// </summary>
		bool IsAvailable { get; }

		/// <summary>
		/// Writes a Tag if available
		/// </summary>
		/// <param name="message">NDEF Message to write on the tag</param>
		void WriteTag(NdefMessage message);

		/// <summary>
		/// Event raised when a tag is discovered and scanned
		/// </summary>
		event EventHandler<NfcFormsTag> NewTag;

		/// <summary>
		/// Event raised when a tag is discovered
		/// </summary>
		event EventHandler<NfcFormsTag> TagConnected;

		/// <summary>
		/// Event raised when a tag is lost
		/// </summary>
		event EventHandler<NfcFormsTag> TagDisconnected;
	}

	public class NfcFormsTag
	{
		public NdefMessage NdefMessage;
		public IList TechList;
		public bool IsNdefSupported;
		public bool IsWriteable;
		public bool IsConnected;
		public byte[] Id;
		public int MaxSize;
	}
}