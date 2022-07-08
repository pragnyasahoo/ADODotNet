
CREATE PROCEDURE [dbo].[InsertStudentRecords]
                (
                @Name VARCHAR(50),
                @Age int
                )
              AS
                INSERT INTO student(StudentName,Age) Values(@Name,@Age)