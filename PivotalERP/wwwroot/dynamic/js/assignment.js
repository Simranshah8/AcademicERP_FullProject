
on_load =()=>{
	
	document.getElementById('assignment-type-form').style.display="none";
	document.getElementById('add-assignment-form').style.display="none";
	// document.getElementById('assignment-list-form').style.display="none";
	document.getElementById('details-form-main').style.display="none";

   
	
}
	window.onload=on_load();

// Radio Button Click
// Add assignment
document.getElementById('add-assignment-type-btn').onclick = function(){
// console.log('add button clicked');
	document.getElementById('assignment-type-section').style.display="none";
	document.getElementById('assignment-type-form').style.display="block";
	// document.getElementById('homework-list').style.display="block";
   
}

document.getElementById('back-assignment-btn').onclick = function(){
	document.getElementById('assignment-type-section').style.display="block";
    
	document.getElementById('assignment-type-form').style.display="none";
}
// Add Assignment Close

// Add Assignment Type
document.getElementById('add-assignment-btn').onclick = function(){
	// console.log('add button clicked');
		document.getElementById('add-assignment-form').style.display="block";
		document.getElementById('assignment-add-section').style.display="none";
	   
	}
	
	document.getElementById('back-add-assignment-btn').onclick = function(){
		document.getElementById('add-assignment-form').style.display="none";
		
		document.getElementById('assignment-add-section').style.display="block";
	}

	document.getElementById('not-submitted-tab').onclick = function(){
		// console.log('add button clicked');
		
			document.getElementById('not-submitted').style.display="display";
		   
		}
	// Details
	document.getElementById('show-details-btn').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('assignment-list-section').style.display="none";
			document.getElementById('details-form-main').style.display="block";
			// document.getElementById('header-tabs').style.display="none";
			// document.getElementById('submitted').style.display="block";
			document.getElementById('not-submitted').style.display="none";
		   
		}

		

		document.getElementById('back-assignment-list-btn').onclick = function(){
			document.getElementById('details-form-main').style.display="none";
			
			document.getElementById('assignment-list-section').style.display="block";
		}

	document.getElementById('not-submitted-tab').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('not-submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
		   
		}
	document.getElementById('submitted-tab').onclick = function(){
		// console.log('add button clicked');
			document.getElementById('not-submitted').style.display="none";
			// document.getElementById('submitted').style.display="block";
			// document.getElementById('submitted').style.display="block";
		   
		}
