# School Management System

This project is a simple School Management System built using ASP.NET Core. The application allows for managing basic information about students, teachers, and subjects, with features for adding, listing, and searching data. The project includes validation, database integration, error handling, and logging.

## Features

  
- **List Students**: 
  - Displays all students class-wise.
  - Provides a search functionality by student name.
 
    
- **List Subjects with Teachers**: 
  - Displays all subjects a student is enrolled in along with their teachers.
  - Supports the following relationships:
    - One teacher can teach multiple subjects.
    - One teacher can teach multiple classes.


## Error Handling & Logging
- The application includes error handling and logging mechanisms to capture and log exceptions to a designated folder (CustomLogging).

  ## Database
  Create a new database in SQL Server named schoolManagement.
  Run the SQL scripts provided in the DBScripts folder to create the necessary tables and relationships.
  
## Design Patterns
- The application architecture follows the repository pattern to manage data access, ensuring separation of concerns and easier testing.

### Steps to Run
1. Clone the repository:
   
   git clone https://github.com/deepboparai/SmartEduManager
