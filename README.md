 .Net Test Task. Level 1.
 
 Task
 
1. Create SQLite database (push database file to the code repository);
2. Create a table in the database which will contain the following fields:
a. Login
b. Password
c. Role (user, admin)
d. USD_balance
3. Implement 3 REST API endpoints:
a. Login. In the case of successful login should return login, role, and balance from
DB for the logged user;
b. Update balance. Users are able to set new balance value for themselves only;
c. Delete user. Users with ADMIN role able to delete any user from database;

Notes
- Absolutely NO UI needed;
- All code should be pushed to a dedicated private GitHub repository. GitHub repository
should be shared with walter@plugg.tech. Make sure you include file for SQLite
database;
- No restrictions on frameworks, libraries to use;
- Candidate should design endpoints structure together with input and output data;
- Provide instruction on how to run the project in the local environment.

Document on pdf: https://drive.google.com/file/d/16icVrrH21GNUoudOuCXe371BGo8dzJU7/view?usp=sharing

Instructions of use:

1. When you run the project, it generates the table User and add 3 users:

Login (user name): user1,
Password: pwd,
Role: admin,
USD_Balance: 10000

Login (user name): user2,
Password: pwd,
Role: user,
USD_Balance: 20000

Login (user name): user3,
Password: pwd,
Role: user,
USD_Balance: 30000

You can find the table on the file: users.sqlite

REST API Endpoints:

1. Login

POST http://localhost:5000/api/users/
{
   "login": "user1",
   "password": "pwd"
}

2. Update Balance

PUT  http://localhost:5000/api/users/
{
   "login": "user1",
   "password": "pwd",
   "balance" : "12345.67"
}
 
3. Delete User

DELETE  http://localhost:5000/api/users/
{
   "login": "user1",
   "password": "pwd",
   "user" : "user2",
}
