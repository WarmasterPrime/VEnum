namespace VEnum
{
	public struct VEnumInfo
	{
		/// <summary>
		/// The <see cref="Enum"/> object.
		/// </summary>
		public readonly Enum EnumObject;
		/// <summary>
		/// Gets the flag names.
		/// </summary>
		public string[] Names => EnumObject.GetInstanceNames();
		/// <summary>
		/// Gets a collection of key-value pairs of the flags.
		/// </summary>
		public Dictionary<string, Enum> KeyValuePairs => EnumObject.GetInstancePairs();
		/// <summary>
		/// Gets an <see cref="Enum"/> array of the flags.
		/// </summary>
		public Enum[] Flags => EnumObject.GetInstanceValues();


		/// <summary>
		/// Creates a new instance of the <see cref="VEnumInfo"/> struct object.
		/// </summary>
		/// <param name="value">An object that inherits from the <see cref="Enum"/> class.</param>
		public VEnumInfo(Enum value) => EnumObject=value;
		/// <inheritdoc cref="VEnumInfo(Enum)"/>
		public static explicit operator VEnumInfo(Enum value) => new(value);

	}
}
