#region License
/* **********************************************************************************
 * Copyright (c) Roman Ivantsov
 * This source code is subject to terms and conditions of the MIT License
 * for Irony. A copy of the license can be found in the License.txt file
 * at the root of this distribution. 
 * By using this source code in any fashion, you are agreeing to be bound by the terms of the 
 * MIT License.
 * You must not remove this notice from this software.
 * **********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Irony.Compiler {
  //Terminal based on custom method; allows creating custom match without creating new class derived from Terminal 
  public delegate Token MatchHandler(Terminal terminal, CompilerContext context, ISourceStream source);
  public class CustomTerminal : Terminal {
    public CustomTerminal(string name, MatchHandler handler, params string[] prefixes) : base(name) {
      _handler = handler;
      if (prefixes != null) 
        Prefixes.AddRange(prefixes);
    }
    
    public readonly KeyList Prefixes = new KeyList();

    public MatchHandler Handler   {
      get {return _handler;}
    } MatchHandler  _handler;

    public override Token TryMatch(CompilerContext context, ISourceStream source) {
      return _handler(this, context, source);
    }
    public override IList<string> GetStartSymbols() {
      return Prefixes;
    }
  }//class


}
