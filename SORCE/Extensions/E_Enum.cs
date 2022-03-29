using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SORCE.Extensions
{
	public static class E_Enum
	{
		public static string GetName<EnumType>(this EnumType enumValue) where EnumType : Enum =>
			Enum.GetName(enumValue.GetType(), enumValue);
	}
}
