public static class NodeHelper
{
    public static UserInterface FindUserInterface(this Node node)
    {
        return node
            .GetTree()
            .GetRoot()
            .GetChild(0)
            .GetNode<UserInterface>("UserInterfaceP1")!;
    }
}
