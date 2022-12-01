namespace RPCGame;

interface IState
{
    string Move(Context context) { return ""; }
    string Attack(Context context) { return ""; }
    string Stop(Context context) { return ""; }
    string Run(Context context) { return ""; }
    string Panic(Context context) { return ""; }
    string CalmDown(Context context) { return ""; }
}

// There are four States
class RestingState : IState
{
    public string Move(Context context)
    {
        context.State = new MovingState();
        return "You start moving";
    }
    public string Attack(Context context)
    {
        context.State = new AttackingState();
        return "You start attacking the darkness";
    }
    public string Stop(Context context)
    {
        return "You are already stopped!";
    }
    public string Run(Context context)
    {
        return "You cannot run unless you are moving";
    }
    public string Panic(Context context)
    {
        context.State = new PanickingState();
        return "You start Panicking and begin seeing things";
    }
    public string CalmDown(Context context)
    {
        return "You are already relaxed";
    }
}

class AttackingState : IState
{
    public string Move(Context context)
    {
        return "You need to stop attacking first";
    }
    public string Attack(Context context)
    {
        return "You attack the darkness for " +
       (new Random().Next(20) + 1) + " damage";
    }
    public string Stop(Context context)
    {
        context.State = new RestingState();
        return "You are calm down and come to rest";
    }
    public string Run(Context context)
    {
        context.State = new MovingState();
        return "You Run away from the fray";
    }
    public string Panic(Context context)
    {
        context.State = new PanickingState();
        return "You start Panicking and begin seeing things";
    }
    public string CalmDown(Context context)
    {
        context.State = new RestingState();
        return "You fall down and sleep";
    }
}

class PanickingState : IState
{
    public string Move(Context context)
    {
        return "You move around randomly in a blind panic";
    }
    public string Attack(Context context)
    {
        return "You start attacking the darkness, but keep on missing";
    }
    public string Stop(Context context)
    {
        context.State = new MovingState();
        return "You are start relaxing, but keep on moving";
    }
    public string Run(Context context)
    {
        return "You run around in your panic";
    }
    public string Panic(Context context)
    {
        return "You are already in a panic";
    }
    public string CalmDown(Context context)
    {
        context.State = new RestingState();
        return "You relax and calm down";
    }
}

class MovingState : IState
{
    public string Move(Context context)
    {
        return "You move around randomly";
    }
    public string Attack(Context context)
    {
        return "You need to stop moving first";
    }
    public string Stop(Context context)
    {
        context.State = new RestingState();
        return "You stand still in a dark room";
    }
    public string Run(Context context)
    {
        return "You run around in cirles";
    }
    public string Panic(Context context)
    {
        context.State = new PanickingState();
        return "You start Panicking and begin seeing things";
    }
    public string CalmDown(Context context)
    {
        context.State = new RestingState();
        return "You stand still and relax";
    }
}

class Context
{
    public IState State { get; set; }

    public void Request(char c)
    {
        string result;
        switch (char.ToLower(c))
        {
            case 'm': result = State.Move(this); break;
            case 'a': result = State.Attack(this); break;
            case 's': result = State.Stop(this); break;
            case 'r': result = State.Run(this); break;
            case 'p': result = State.Panic(this); break;
            case 'c': result = State.CalmDown(this); break;
            case 'e': result = "Thank you for playing \"The RPC Game\""; break;
            default: result = "Error, try again"; break;
        }
        Console.WriteLine(result);
    }
}

public class Program
{
    // The user interface
    static void Main(string[] args)
    {
        // context.s are States
        // Decide on a starting state and hold onto the Context thus established
        Context context = new Context();
        context.State = new RestingState();

        char command = ' ';
        Console.WriteLine("Welcome to \"The State Game\"!");
        Console.WriteLine("You are standing here looking relaxed!");
        while (command != 'e')
        {
            Console.WriteLine("\nWhat would you like to do now?");
            Console.Write(@$" Move -> 'm' / Attack-> 'a' / Stop -> 's' / Run -> 'r' /
Panic -> 'p' / CalmDown -> 'c' / Exit-> 'e'  
the game: ==> ");
            string choice;
            do
                choice = Console.ReadLine();
            while (choice==null);
            command = choice[0];
            context.Request(command);
        }
    }

}
