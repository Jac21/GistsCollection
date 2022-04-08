class Types {
  static Text = new Types("text");
  static File = new Types("file");
  static Folder = new Types("folder");

  constructor(name) {
    this.name = name;
  }
}

export default Types;
