defmodule Portal.Door do
	@doc """
	Starts a door with the given state

	State is given as name for identification

	iex()> Portal.Door.start_link(:blue)
	"""
	def start_link(color) do
		Agent.start_link(fn -> [] end, name: color)
	end

	@doc """
	Get the data currently in the door

	iex()> Portal.Door.get(:blue)
	"""
	def get(door) do
		Agent.get(door, fn list -> list end)
	end

	@doc """
	Pushes value into the door

	iex()> Portal.Door.push(:blue, 21)
	"""
	def push(door, value) do
		Agent.update(door, fn list -> [value|list] end)
	end

	@doc """
	Pops a value from the door

	Returns {:ok, value} if there is a value,
	:error if empty

	iex()> Portal.Door.pop(:blue)
	"""
	def pop(door) do
		Agent.get_and_update(door, fn
			[]		-> {:error, []}
			[h|t] 	-> {{:ok, h}, t}
		end)
	end	
end