chrome.commands.onCommand.addListener(function(command) {

	switch (command){
		case 'add':
			add();
			break;
		case 'myCommand2':
			get();
			break;
	}
});

function add (){
	
	chrome.tabs.getSelected(null, function(tab) {
		
		// ページタイトル取得
		var title = tab.title;
		
		// URL取得
		var url = tab.url;

		var obj = {
			cmd : 'add',
			title : title,
			url : url
		};
		
		var json = 'json=' + JSON.stringify(obj);

		// 表示
		window.alert(json);

		// サーバーに情報を問い合わせ
		$.ajax({
			type: "POST",
			url: "http://localhost:8888",
			data: json
		}).done(function(msg){
			
			// 表示
			window.alert(msg);

		}).fail(function(data, textStatus, errorThrown){
			alert(textStatus); //エラー情報を表示
			alert(errorThrown.message); //例外情報を表示
			//alert('error!!!');
		});
	});
}

function get (){

	var obj = {
		cmd : 'get'
	};
	
	var json = 'json=' + JSON.stringify(obj);

	// 表示
	window.alert(json);

	// サーバーに情報を問い合わせ
	$.ajax({
		type: "POST",
		url: "http://localhost:8888",
		data: json
	}).done(function(msg){
		
		// 表示
		window.alert(msg);
		
	}).fail(function(data, textStatus, errorThrown){
		alert(textStatus); //エラー情報を表示
		alert(errorThrown.message); //例外情報を表示
		//alert('error!!!');
	});
}