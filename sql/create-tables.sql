CREATE TABLE Boards (
    Id TEXT,
    BoardStr TEXT,
    PRIMARY KEY(id)
);

CREATE TABLE BoardWords (
    BoardId TEXT,
    Word TEXT,
    PRIMARY KEY(BoardId, Word)
);
