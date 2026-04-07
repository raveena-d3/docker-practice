function Inputs(props) {
  return (
    <div>
      <label>{props.label}</label>
      <input
        type={props.type}
        value={props.value}
        onChange={(e) => props.getTask(e.target.value)}
      />
    </div>
  );
}

export default Inputs;