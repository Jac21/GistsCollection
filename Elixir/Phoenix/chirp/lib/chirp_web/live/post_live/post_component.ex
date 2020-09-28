defmodule ChirpWeb.PostLive.PostComponent do
  use ChirpWeb, :live_component

  def render(assigns) do
    ~L"""
      <div id="post-<%= @post.id %>" class="post">
        <div class="row">
          <div><%= @post.username %></div>
          <div><%= @post.body %></div>

          <a href="#" phx-click="like" phx-target="<%= @myself %>">
            <i class="far fa-heart"></i> <%= @post.likes_count %>
          </a>

          <a href="#" phx-click="repost" phx-target="<%= @myself %>">
            <i class="far fa-retweet"></i> <%= @post.reposts_count %>
          </a>
        </div>
      </div>
  """
  end

  def handle_event("like", _, socket) do
    Chirp.Timeline.inc_likes(socket.assigns.post)
    {:noreply, socket}
  end

  def handle_event("repost", _, socket) do
    Chirp.Timeline.inc_reposts(socket.assigns.post)
    {:noreply, socket}
  end
end
