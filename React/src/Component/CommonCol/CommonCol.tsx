import { Button, Form } from "react-bootstrap";
import { CommonColProps } from "./interface";
import "./CommonCol.css";

const CommonCol: React.FC<CommonColProps> = (props) => {
  const { label, buttonContent, onClickFunc, result } = props;
  return (
    <Form.Group className="common-col-container">
      <Button className="common-button" onClick={onClickFunc}>
        {buttonContent}
      </Button>
      <div>
        <Form.Label className="fs-6 mb-0">{label}</Form.Label>
        <div className="border border-secondary command-result">{result}</div>
      </div>
    </Form.Group>
  );
};

export default CommonCol;
