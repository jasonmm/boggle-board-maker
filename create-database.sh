#!/bin/sh

rm boggle-boards.db
cat sql/create-tables.sql | sqlite3 boggle-boards.db
