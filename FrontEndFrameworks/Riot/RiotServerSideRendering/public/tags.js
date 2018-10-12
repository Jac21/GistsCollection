riot.tag2('hello-world', '<h2>Hello, {opts.firstName}</h2> <input type="text" name="fName"> <button onclick="{updateName}">Update Name</button>', '', '', function(opts) {
		this.updateName = function() {
			opts.firstName = this.fName.value;
			this.fName.value = '';
		}.bind(this);
});