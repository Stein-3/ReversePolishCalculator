import PushFormula from "../PushFormula/PushFormula";
import Stack from "../Stack/Stack";
import "./Content.css";

const Content: React.FC = () => {
  return (
    <div className="wrapper">
      <div className="operation-area">
        <PushFormula />
      </div>
      <div className="stack-area">
        <Stack />
      </div>
    </div>
  );
};

export default Content;
