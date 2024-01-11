# maelstrom-kafka

## "send" message from Maelstrom -

```json
{
  "type": "send",
  "key": "k1",
  "msg": 123
}
```

## "send_ok" message reply -

```json
{
  "type": "send_ok",
  "offset": 1000
}
```

## "poll" message from Maelstrom -

```json
{
  "type": "poll",
  "offsets": {
    "k1": 1000,
    "k2": 2000
  }
}
```

## "poll_ok" message reply -

```json
{
  "type": "poll_ok",
  "msgs": {
    "k1": [
      [1000, 9],
      [1001, 5],
      [1002, 15]
    ],
    "k2": [
      [2000, 7],
      [2001, 2]
    ]
  }
}
```

## "commit_offsets" message from Maelstrom -

```json
{
  "type": "commit_offsets",
  "offsets": {
    "k1": 1000,
    "k2": 2000
  }
}
```

## "commit_offsets_ok" message reply -

```json
{
  "type": "commit_offsets_ok"
}
```

## "list_committed_offsets" message from Maelstrom -

```json
{
  "type": "list_committed_offsets",
  "keys": ["k1", "k2"]
}
```

## "list_committed_offsets_ok" message reply -

```json
{
  "type": "list_committed_offsets_ok",
  "offsets": {
    "k1": 1000,
    "k2": 2000
  }
}
```
