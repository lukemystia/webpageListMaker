chrome.commands.onCommand.addListener(function(command) {

	switch (command){
		case 'add':
			add();
			break;
		case 'get':
			
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
			if (msg == 'notfound') {
				return;
			}
		});
	});
}