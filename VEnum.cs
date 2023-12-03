namespace VEnum
{
	public class VEnum
	{
		/// <summary>
		/// The <see cref="Enum"/> object.
		/// </summary>
		private readonly Enum Value;
		/// <summary>
		/// The instance information of the <see cref="Enum"/>.
		/// </summary>
		public readonly VEnumInfo InstanceInfo;
		/// <summary>
		/// The base information of the <see cref="Enum"/>.
		/// </summary>
		public readonly VEnumInfo BaseInfo;


		/// <summary>
		/// Creates a new instance of the <see cref="VEnum"/> class.
		/// </summary>
		/// <param name="value">an <see cref="Enum"/> object.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public VEnum(Enum value)
		{
			if(value is null)
				throw new ArgumentNullException(nameof(value));
			Value=value;
			InstanceInfo=new(value);
			BaseInfo=new(value.GetBaseValues().CollapseEnumArray());
		}
		/// <inheritdoc cref="VEnum(Enum)"/>
		public static implicit operator VEnum(Enum value) => new(value);

	}
}