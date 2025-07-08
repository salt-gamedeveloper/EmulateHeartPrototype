public static class FileIdGenerator
{
    public static string GetCharacterExpressionId(CharacterType characterType, CharacterExpression expression)
    {
        return $"{Enums.CharacterTypeToLowerString(characterType)}_{Enums.CharacterExpressionToLowerString(expression)}";
    }

    public static string GetBGId(LocationType location, TimeOfDay time)
    {
        return $"{Enums.LocationTypeToLowerString(location)}_{Enums.TimeOfDayToLowerString(time)}";
    }
}