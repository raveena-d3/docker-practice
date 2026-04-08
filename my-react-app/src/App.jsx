import React from 'react';
import './App.css';
import Inputs from './input';

const API_URL = "/api/tasks";

function App() {
  const [task, setTask] = React.useState("");
  const [taskList, setTaskList] = React.useState([]);
  const [editId, setEditId] = React.useState(null);

  React.useEffect(() => {
    fetch(API_URL)
      .then((res) => res.json())
      .then((data) => setTaskList(data))
      .catch((err) => console.error("Failed to load tasks:", err));
  }, []);

  const addOrUpdateTask = async () => {
    if (task === "") {
      alert("Please enter a task");
      return;
    }
    if (editId !== null) {
      const res = await fetch(`${API_URL}/${editId}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title: task }),
      });
      const updated = await res.json();
      setTaskList((prev) => prev.map((t) => (t.id === editId ? updated : t)));
      setEditId(null);
    } else {
      const res = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ title: task }),
      });
      const created = await res.json();
      setTaskList((prev) => [...prev, created]);
    }
    setTask("");
  };

  const deleteTask = async (id) => {
    await fetch(`${API_URL}/${id}`, { method: "DELETE" });
    setTaskList((prev) => prev.filter((t) => t.id !== id));
    if (editId === id) { setTask(""); setEditId(null); }
  };

  const editTask = (item) => {
    setTask(item.title);
    setEditId(item.id);
  };

  return (
    <div>
      <h2>To-Do App</h2>
      <Inputs label="Task" type="text" value={task} getTask={setTask} />
      <button onClick={addOrUpdateTask}>
        {editId !== null ? "Update" : "Add"}
      </button>
      {taskList.map((item) => (
        <div key={item.id} className="task-item">
          <p>{item.title}</p>
          <button onClick={() => editTask(item)}>Edit</button>
          <button onClick={() => deleteTask(item.id)}>Delete</button>
        </div>
      ))}
    </div>
  );
}

export default App;