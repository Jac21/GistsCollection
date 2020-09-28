# Hello

To start your Phoenix server:

  * Install dependencies with `mix deps.get`
  * Create and migrate your database with `mix ecto.setup`
  * Install Node.js dependencies with `cd assets && npm install`
  * Start Phoenix endpoint with `mix phx.server`

Now you can visit [`localhost:4000`](http://localhost:4000) from your browser.

Creating some resources?

  * Example command to create a CRUD "Users" resource - `mix phx.gen.html Accounts User users name:string username:string:unique`
  * Make sure to run `mix ecto.migrate`
  * Edit lib/hello_web/router.ex by adding `resouces "/users", UserController` within `scope "/", HelloWeb do`
  * Test with `mix test` in root directory

Ready to run in production? Please [check our deployment guides](https://hexdocs.pm/phoenix/deployment.html).

## Learn more

  * Official website: http://www.phoenixframework.org/
  * Guides: https://hexdocs.pm/phoenix/overview.html
  * Docs: https://hexdocs.pm/phoenix
  * Mailing list: http://groups.google.com/group/phoenix-talk
  * Source: https://github.com/phoenixframework/phoenix
