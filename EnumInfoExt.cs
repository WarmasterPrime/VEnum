using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VEnum
{
	/// <summary>
	/// Provides information related to an <see cref="Enum"/>.
	/// </summary>
	public static class EnumInfoExt
	{
		/// <summary>
		/// Gets the names of the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">An <see cref="Enum"/> object.</typeparam>
		/// <param name="source">The <see cref="Enum"/> object to analyze.</param>
		/// <returns>a <see cref="string"/> array.</returns>
		public static string[] GetBaseNames<TEnum>(this TEnum source) where TEnum : Enum => Enum.GetNames(typeof(TEnum));
		/// <inheritdoc cref="GetBaseNames{TEnum}(TEnum)"/>
		/// <summary>
		/// Gets the values within the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <returns>an array of all the <paramref name="source"/> flags.</returns>
		public static TEnum[] GetBaseValues<TEnum>(this TEnum source) where TEnum : Enum => (TEnum[])Enum.GetValues(typeof(TEnum));
		/// <inheritdoc cref="GetBaseNames{TEnum}(TEnum)"/>
		/// <summary>
		/// Gets the values within the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <returns>a <see cref="string"/> array of the names within the <paramref name="source"/>.</returns>
		public static string[] GetInstanceNames<TEnum>(this TEnum source) where TEnum : Enum
		{
			var list=source.GetInstanceValues();
			string[] res=new string[list.Length];
			Type type=typeof(TEnum);
			for(int i=0;i<list.Length;i++)
				res[i]=Enum.GetName(type, list[i])!;
			return res;
		}
		/// <summary>
		/// Determines if the <paramref name="source"/> (instance) contains all of the <paramref name="values"/>.
		/// </summary>
		/// <typeparam name="TEnum">An <see cref="Enum"/> object.</typeparam>
		/// <param name="source">The <see cref="Enum"/> object to analyze.</param>
		/// <param name="values">An array of the flags to look for.</param>
		/// <returns>a <see cref="bool">boolean</see> value representing the success status of the operation.</returns>
		public static bool HasAll<TEnum>(this TEnum source, params TEnum[] values) where TEnum : Enum => values.All(q=>source.HasFlag(q));
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		public static bool HasAll<TEnum>(this TEnum source, params string[] values) where TEnum : Enum => source.HasAll(source.ToEnumArray(values));
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Determines if the <paramref name="source"/> instance contains any of the <paramref name="values"/>.
		/// </summary>
		public static bool HasAny<TEnum>(this TEnum source, params TEnum[] values) where TEnum : Enum => values.Any(q=>source.HasFlag(q));
		/// <inheritdoc cref="HasAny{TEnum}(TEnum, TEnum[])"/>
		public static bool HasAny<TEnum>(this TEnum source, params string[] values) where TEnum : Enum => source.HasAny(source.ToEnumArray(values));
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>Determines if the <paramref name="source"/> (base) contains all of the <paramref name="values"/>.</summary>
		public static bool ContainsAll<TEnum>(this TEnum source, params TEnum[] values) where TEnum : Enum => source.GetBaseValues().CollapseEnumArray().HasAll(values);
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Determines if the <paramref name="source"/> (base) contains any of the <paramref name="values"/>.
		/// </summary>
		public static bool ContainsAny<TEnum>(this TEnum source, params TEnum[] values) where TEnum : Enum => source.GetBaseValues().CollapseEnumArray().HasAny(values);
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Generates a <typeparamref name="TEnum"/> representation of the <paramref name="source"/>.
		/// </summary>
		/// <returns>a <typeparamref name="TEnum"/>.</returns>
		public static TEnum CollapseEnumArray<TEnum>(this TEnum[] source) where TEnum : Enum
		{
			TEnum res=source[0];
			foreach(var sel in source)
				res.Add(sel);
			return res;
		}
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Adds <paramref name="values"/> to the <paramref name="source"/>.
		/// </summary>
		/// <returns>the modified <see cref="Enum"/> object.</returns>
		public static TEnum Add<TEnum>(this TEnum? source, params TEnum[] values) where TEnum : Enum
		{
			source??=values[0];
			long res=Convert.ToInt64(source);
			foreach(var sel in values)
				if(!source.HasFlag(sel))
					res|=Convert.ToInt64(sel);
			return (TEnum)Enum.ToObject(typeof(TEnum), res);
		}
		/// <inheritdoc cref="Add{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Removes a flag from the instance <paramref name="source"/>.
		/// </summary>
		public static TEnum? Remove<TEnum>(this TEnum source, params TEnum[] values) where TEnum : Enum
		{
			TEnum[] list=source.GetInstanceValues();
			if(values is null || values.Length<0 || values.Length>=list.Length)
				return default;
			TEnum[] res=Array.Empty<TEnum>();
			foreach(var sel in list)
				if(!source.HasAny(values))
				{
					Array.Resize(ref res, res.Length+1);
					res[^1]=sel;
				}
			return res.CollapseEnumArray();
		}
		/// <inheritdoc cref="GetBaseNames{TEnum}(TEnum)"/>
		/// <summary>
		/// Gets the flags that the current instance contains, as an array of those flags.
		/// </summary>
		/// <returns>an array of all the flags found within the <paramref name="source"/>.</returns>
		public static TEnum[] GetInstanceValues<TEnum>(this TEnum source) where TEnum : Enum
		{
			TEnum[] list=(TEnum[])Enum.GetValues(typeof(TEnum));
			TEnum[] res=Array.Empty<TEnum>();
			foreach(TEnum sel in list)
				if(source.HasFlag(sel))
				{
					Array.Resize(ref res, res.Length+1);
					res[^1]=sel;
				}
			return res;
		}
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Gets the name of a flag.
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="source"></param>
		/// <returns>a <see cref="string"/> representation of the flag name.</returns>
		public static string GetInstanceName<TEnum>(this TEnum source) where TEnum : Enum => Enum.GetName(typeof(TEnum), source)!;
		/// <summary>
		/// Gets the key-value pairs of the <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static Dictionary<string, TEnum> GetInstancePairs<TEnum>(this TEnum source) where TEnum : Enum => source.GetInstanceValues().Behind_GetBasePairs();
		/// <inheritdoc cref="GetInstancePairs{TEnum}(TEnum)"/>
		public static Dictionary<string, TEnum> GetBasePairs<TEnum>(this TEnum source) where TEnum : Enum => source.GetBaseValues().Behind_GetBasePairs();

		private static Dictionary<string, TEnum> Behind_GetBasePairs<TEnum>(this TEnum[] sourceList) where TEnum : Enum
		{
			Dictionary<string, TEnum> res=new();
			foreach(var sel in sourceList)
				res.Add(sel.GetInstanceName(), sel);
			return res;
		}
		/// <inheritdoc cref="HasAll{TEnum}(TEnum, TEnum[])"/>
		/// <summary>
		/// Generates a <typeparamref name="TEnum"/>[] array from the <paramref name="values"/>.
		/// </summary>
		/// <typeparam name="TEnum">An <see cref="Enum"/> object.</typeparam>
		/// <param name="source">An instance of an <see cref="Enum"/> object (Will only use it's base flags, not it's instance flags).</param>
		/// <param name="values">A <see cref="string"/> representation of the flag name.</param>
		/// <returns>a <typeparamref name="TEnum"/>[] array representation of all the <paramref name="values"/>.</returns>
		public static TEnum[] ToEnumArray<TEnum>(this TEnum source, params string[] values) where TEnum : Enum
		{
			TEnum[] res=Array.Empty<TEnum>();
			var l=source.GetInstancePairs();
			foreach(var sel in values)
				if(l.ContainsKey(sel))
				{
					Array.Resize(ref res, res.Length+1);
					res[^1]=l[sel];
				}
			return res;
		}

	}
}
