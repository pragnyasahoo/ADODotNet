Create procedure SpMyProcedure ( 
@Id int,
@Name varchar(100)= null,  
@Age varchar(100)= null, 
@Action varchar(100)= null  
) 

As 
BEGIN
	if @Action = 'Insert' 
		Insert into student (StudentName, Age) values (@Name, @Age) 
	if @Action = 'Update'  
		Update student  set  StudentName = @Name,  Age = @Age  where Id = @Id 
	if @Action = 'Delete'  
		Delete from student  where  Id = @Id 
END  