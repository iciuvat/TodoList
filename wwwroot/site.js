const uri = 'api/todoitems';
let todos = [];

function getItems() {
  fetch(uri)
    .then(response => response.json())
    .then(data => _displayItems(data))
    .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
  const addNameTextbox = document.getElementById('add-name');

  const item = {
    isComplete: false,
    name: addNameTextbox.value.trim()
  };

  fetch(uri, {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
  })
    .then(response => response.json())
    .then(() => {
      getItems();
      addNameTextbox.value = '';
    })
    .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
  fetch(`${uri}/${id}`, {
    method: 'DELETE'
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to delete item.', error));
}

function completeIt(id) {
  const item = todos.find(item => item.id === id);
  
  const itemCompleted = {
    id: item.id,
    isComplete: true,
    name: item.name
  };  

  fetch(`${uri}/${item.id}`, {
    method: 'PUT',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(itemCompleted)
  })
  .then(() => getItems())
  .catch(error => console.error('Unable to update item.', error));

  return false;
}

function _displayCount(itemCount) {
  document.getElementById('counter').innerText = `Todo list (${itemCount})`;
}

function _displayItems(data) {
  const aExport = document.getElementById('aExport');
  if (data.length == 0) {
    aExport.href = "javascript:void(0);";
    aExport.style="pointer-events: none; color: lightgray;";
  }
  else {
    aExport.href="data:application/json;charset=utf-8," + JSON.stringify(data, false, 4);
    aExport.style="";
  }
  

  const tBody = document.getElementById('todos');
  tBody.innerHTML = '';

  _displayCount(data.length);

  const button = document.createElement('button');

  data.forEach(item => {
    let isCompleteCheckbox = document.createElement('input');
    isCompleteCheckbox.type = 'checkbox';
    isCompleteCheckbox.disabled = true;
    isCompleteCheckbox.checked = item.isComplete;

    let completeItButton = button.cloneNode(false);
    completeItButton.innerText = 'Complete It!';
    completeItButton.setAttribute('onclick', `completeIt("${item.id}")`);
    completeItButton.disabled = item.isComplete;

    let tr = tBody.insertRow();
    
    let td1 = tr.insertCell(0);
    td1.appendChild(isCompleteCheckbox);

    let td2 = tr.insertCell(1);
    let textNode = document.createTextNode(item.name);
    td2.appendChild(textNode);

    let td3 = tr.insertCell(2);
    td3.appendChild(completeItButton);
  });

  todos = data;
}