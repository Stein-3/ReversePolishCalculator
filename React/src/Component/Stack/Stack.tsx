import React from "react";
import { ListGroup } from "react-bootstrap";
import { useRecoilState } from "recoil";
import { displayStack } from "../../Common/RPNApiCaller";
import { StackState } from "../../Store/StackAtom";
import Presenter from "./Presenter";
import "./Stack.css";

const Stack = () => {
  const [stack, setStack] = useRecoilState(StackState);

  React.useEffect(() => {
    displayStack()
      .then((result) => {
        setStack(result);
      })
      .catch((e) => {
        setStack(["Stackの更新に失敗しました"]);
      });
  }, [setStack]);

  const children = stack?.map((item, key) => (
    <ListGroup.Item key={key}>{item}</ListGroup.Item>
  ));

  return <Presenter children={children} />;
};

export default Stack;
