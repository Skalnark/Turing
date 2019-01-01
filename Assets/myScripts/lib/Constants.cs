public static class Constants
{
    public const int NORMAL = 0;
    public const int FINAL = 1;
    public const int INITIAL = 2;
    public const int ACCEPT = 3;
    public const int REJECT = 4;

    //This last one is for the alphabet key
    public const int WHITESPACE = 0;

    static string description = "{Beaver4#1#0#2#defv,1,R,1;1,1,L,1;defv,1,L,0;1,v,L,2;defv,1,R,2;1,1,L,3;defv,1,R,3;1,v,R,0;#Implementationofthe4-statesbusybeavermachine.}{Beaver6#1#0#5#defv,1,R,1;1,v,L,4;defv,1,L,2;1,v,R,0;defv,1,L,3;1,v,R,2;defv,1,L,4;1,v,L,5;defv,1,L,0;1,1,L,2;defv,1,L,4;1,1,R,5;#Implementationofthe6-statesbusybeavermachine.}";

    public static string DEFAULTDESCRIPTIONS()
    {
        return description;
        
    }
}