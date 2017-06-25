﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClownFish.HttpTest
{
	internal static class BinSerializerHelper
	{
		public static byte[] Serialize(object obj)
		{
			if( obj == null )
				throw new ArgumentNullException(nameof(obj));

			using( MemoryStream stream = new MemoryStream() ) {
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, obj);

				stream.Position = 0;
				return stream.ToArray();
			}
		}


		public static T Deserialize<T>(byte[] buffer)
		{
			return (T)DeserializeObject(buffer);
		}

		public static object DeserializeObject(byte[] buffer)
		{
			if( buffer == null )
				throw new ArgumentNullException(nameof(buffer));

			using( MemoryStream stream = new MemoryStream(buffer) ) {
				stream.Position = 0;

				BinaryFormatter formatter = new BinaryFormatter();
				return formatter.Deserialize(stream);
			}
		}


		public static T CloneObject<T>(this T obj)
		{
			byte[] bb = Serialize(obj);
			return Deserialize<T>(bb);
		}
	}
}
