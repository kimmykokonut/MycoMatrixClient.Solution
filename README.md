# Communicating with an API
### by Henry O. & Kim R.

## Prompt
* API-Consuming Application
 Build an application that interacts with an API (using ![myco-matrix-api](https://github.com/kimmykokonut/myco-matrix)). Your goal is twofold: continue improving the API itself and make an application for users to interact with!

 A user can make these API calls:
 - see the list of mushrooms (GetAll)
 - see an individual mushroom's details (Get by id)
 - create a mushroom object (Post)
 - edit an individual mushroom (Put)
 - delete an individual mushroom (Delete)

 21 feb 2024
 full CRUD for UI to interact with API.

 ### issues: had to remove authorization, JWT token and editor check in controller.  without those in place, CRUD achieved.