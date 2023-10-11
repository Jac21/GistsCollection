# maelstrom-broadcast

## RPC 'broadcast' message from Maelstrom -

```json
{
  "type": "broadcast",
  "message": 1000
}
```

## Message reply -

```json
{
  "type": "broadcast_ok"
}
```

## RPC 'read' message from Maelstrom -

```json
{
  "type": "read"
}
```

## Message reply with list of values node has seen -

```json
{
  "type": "read_ok",
  "messages": [1, 8, 72, 25]
}
```

## RPC 'topology' message from Maelstrom -

```json
{
  "type": "topology",
  "topology": {
    "n1": ["n2", "n3"],
    "n2": ["n1"],
    "n3": ["n1"]
  }
}
```

## Message reply -

```json
{
  "type": "topology_ok"
}
```
