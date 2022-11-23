### Task

Implement a web application for organizing content into folders. It should satisfy the following user stories:

- As a user I want to be able to create new objects:
    - Video with a custom title
    - Folder with a custom title
- As a user I want to be able to organize videos into folders:
    - User can move video into a folder
    - User can move `folder1` into `folder2`
    - User can move video that is nested in the `folder1` one level up
    - User can move `folder1` that is nested in the `folder2` one level up
- As a user I want to be able to navigate between folders and see their content
- As a user I want to have a human-readable link that points directly to a particular folder (so that if I share it with a friend, he would directly see this folder’s content)

**Things you don’t have to worry about:**

- CI configuration / Deployment
- Authentication / Authorization