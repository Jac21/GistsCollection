if (navigator.requestMIDIAccess) {
  console.log("This browser supports WebMIDI!");

  var onMIDISuccess = function(midiAccess) {
    console.log(midiAccess);

    for (var input of midiAccess.inputs.values()) {
      input.onmidimessage = getMIDIMessage;
    }
  };

  var getMIDIMessage = function(message) {
    console.log(message);

    var command = message.data[0];
    var note = message.data[1];
    var velocity = message.data.length > 2 ? message.data[2] : 0; // a velocity value might not be included with a noteOff command

    switch (command) {
      case 144: // noteOn
        if (velocity > 0) {
          noteOn(note, velocity);
        } else {
          noteOff(note);
        }
        break;
      case 128: // noteOff
        noteOff(note);
        break; // we could easily expand this switch statement to cover other types of commands such as controllers or sysex
    }
  };

  var onMIDIFailure = function() {
    console.log("Could not access your MIDI devices.");
  };

  navigator.requestMIDIAccess().then(onMIDISuccess, onMIDIFailure);
} else {
  console.log("WebMIDI is not supported in this browser.");
}
