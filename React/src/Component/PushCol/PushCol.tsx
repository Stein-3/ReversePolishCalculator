import { Button, Form } from "react-bootstrap";
import { PushColProps } from "./interface";
import "./PushCol.css";

const PushCol: React.FC<PushColProps> = (props) => {
  return (
    <Form.Group className="push-col-container">
      <div>
        <Form.Label className="fs-6 mb-0">Stackに送る式</Form.Label>
        <Form.Control
          type="text"
          value={props.content}
          onChange={props.onInputChange}
        />
      </div>
      <Button className="push-button" onClick={props.onButtonClick}>
        Push
      </Button>
    </Form.Group>
  );
};

export default PushCol;
