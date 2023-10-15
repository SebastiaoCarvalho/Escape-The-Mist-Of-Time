public class Item {
    private string _name;
    public string Name { get => _name; }
    private string _description;

    public Item(string name, string description) {
        _name = name;
        _description = description;
    }
}