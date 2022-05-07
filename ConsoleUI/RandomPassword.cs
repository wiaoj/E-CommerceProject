using Core.Utilities.Toolkit;

static String FirstReverse(String? text) {
	if(text is null) {
		return "null";
	}
	Char[] charArray = text.ToCharArray();
	text = default;
	for(int i = charArray.Length - 1; i >= 0; i--) {
		text += charArray[i];
	}
	return text;
}

Console.WriteLine(Byte.MaxValue);
Console.WriteLine(UInt16.MinValue);
Console.WriteLine(UInt16.MaxValue);
Console.WriteLine(Int16.MinValue);
Console.WriteLine(Int16.MaxValue);
Console.WriteLine(Single.MaxValue);

for(int i = 0; i < 1_000_000; i++) {
	Console.Write(RandomGenerator.RandomNumberGenerator());
	//Thread.Sleep(100);
	Console.Write(RandomGenerator.RandomFloatNumberGenerator());
	//Thread.Sleep(100);
	Console.Write(RandomGenerator.RandomDoubleNumberGenerator());
	//Thread.Sleep(1000);
	Console.Write(RandomGenerator.RandomPasswordGenerator(Byte.MaxValue));
	//Thread.Sleep(1000);
}