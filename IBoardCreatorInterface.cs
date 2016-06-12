namespace BoggleBoardMaker
{
    public interface IBoardCreatorInterface
    {
        BoggleBoard Create(BoardCreationOptions options);
    }
}