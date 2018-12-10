using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Security.Cryptography;

namespace anyBaseFileReader
{
	class Form1: Form
	{
		
		[STAThread]
		static void Main()
		{
			string[] args = Environment.GetCommandLineArgs();
			string file_to_parse = "";
			int interpret_as = 0;
			int read_as = 0;
			string all_chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
			
			try
			{
				file_to_parse = args[1];
				read_as = Int32.Parse(args[2]);
				interpret_as = Int32.Parse(args[3]);
			} catch 
			{
				Console.WriteLine("Not enough arguments");
				Console.WriteLine("<Cmd> <path to file to parse> <read as> <interpret as>");
				return;
			}
			
			byte[] hex_data;
			
			
			try
			{
				hex_data = File.ReadAllBytes(file_to_parse);
			}
			catch
			{
				Console.WriteLine("Unable to read input file");
				return;
			}
			
			byte[] bits = new byte[hex_data.Length * 8];
			
			int k = 0;
			for (int i = 0; i < hex_data.Length; i ++)
			{
				for (int j = 0; j < 8; j ++)
				{
					bits[k] = (byte) ((hex_data[i] >> j) & 1);
					k = k + 1;
				}
			}

			if (bits.Length % read_as != 0)
			{
				int old_len = bits.Length;
				int padding_length = (((bits.Length / read_as) + 1) * read_as) - bits.Length;
				byte[] padding = new byte[padding_length];
				Array.Clear(padding, 0, padding.Length);
				
				Array.Resize(ref bits, bits.Length + padding_length);
				Array.Copy(padding, 0, bits, old_len, padding_length);
			}

			int start_index = 0;

			// Console.WriteLine("Total Length in bits = {0}", bits.Length);
			
			string final_result = "";
			int runs_of_loop = 1;
			for (int i = 0; i < bits.Length / read_as; i ++)
			{
				byte[] block = new byte[read_as];

				Array.Copy(bits, start_index, block, 0, read_as);
				start_index = start_index + read_as;
				
				long result_int = 0;

				for (int j = 0; j < read_as; j ++)
				{
					result_int = result_int + (long) ((block[j] & 1) * Math.Pow(2, j));
				}
				
				string result_str = "";
				
				while (result_int != 0)
				{
					int rem = (int) (result_int % interpret_as);
					result_str = result_str + all_chars[rem];
					result_int = result_int / interpret_as;
				}
				char[] arr = result_str.ToCharArray();
        			Array.Reverse(arr);
        			result_str = new string(arr);
        			
				Console.Write("{0, -15}", result_str);
				
				if ((runs_of_loop >= 4) & ((runs_of_loop % 4) == 0) & ((runs_of_loop % 8) != 0))
				{
					Console.Write("{0, -15}", "-");
				}
				if ((runs_of_loop >= 8) & ((runs_of_loop % 8) == 0))
				{
					Console.Write("\n");
				}
				runs_of_loop = runs_of_loop + 1;
			}
			Console.WriteLine(final_result);
		}
	}
}
