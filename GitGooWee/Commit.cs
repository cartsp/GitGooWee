namespace GitGooWee
{
    public class Commit
    {
        public string Hash { get; set; }
        public string Message { get; set; }
        
        public override string ToString() => 
            $"+ {Hash[0..5]}.. {Message}";
    }
}