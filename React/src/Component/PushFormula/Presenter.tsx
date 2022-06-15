import React from "react";
import CommonCol from "../CommonCol/CommonCol";
import PushCol from "../PushCol/PushCol";
import { PushFormulaProps } from "./interface";

const Presenter: React.FC<PushFormulaProps> = (props) => {
  const { push, pop, clear, run } = props;
  return (
    <form onSubmit={push.onButtonClick}>
      <PushCol
        content={push.content}
        onInputChange={push.onInputChange}
        onButtonClick={push.onButtonClick}
      />
      <CommonCol
        label="Stackから取り出された値"
        buttonContent="Pop"
        onClickFunc={pop.onClickFunc}
        result={pop.result}
      />
      <CommonCol
        label="結果"
        buttonContent="Clear"
        onClickFunc={clear.onClickFunc}
        result={clear.result}
      />
      <CommonCol
        label="実行結果"
        buttonContent="Run"
        onClickFunc={run.onClickFunc}
        result={run.result}
      />
    </form>
  );
};

export default Presenter;
